using System.Linq;
using System.Threading.Tasks;
using API;
using API.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPI_Detailed.DTO;
using RestAPI_Detailed.Interfaces;

namespace RestAPI_Detailed.Controllers
{
    public class PhotoController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly IPhotoAccessor _photoAccessor;
        private readonly IUserAccessor _userAccessor;
        public PhotoController(DataContext context, IPhotoAccessor photoAccessor, IUserAccessor userAccessor)
        {
            this._userAccessor = userAccessor;
            this._photoAccessor = photoAccessor;
            this._context = context;
        }

        [HttpPost("upload")]
        public async Task<ActionResult<Photo>> UploadPhoto([FromForm] IFormFile File)
        {
            var user = await _context.Users.Include(x=> x.Photos).SingleOrDefaultAsync(x => x.UserName == _userAccessor.GetUserName());
            
            if(user == null){
                return Unauthorized();
            }
            var upload = await _photoAccessor.UploadPhoto(File);

            if(upload ==null){
                return BadRequest("Failed to upload image.");
            }
            
            var photo = new Photo{
                Id = upload.PhotoId,
                Url = upload.PhotoUrl
            };
            if(!user.Photos.Any(x=> x.IsMain==true)){
                photo.IsMain =true;
            }

            user.Photos.Add(photo);

            await _context.SaveChangesAsync();

            return photo;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeletePhoto(string id){
            var user = await _context.Users.Include(x=> x.Photos).SingleOrDefaultAsync(x => x.UserName == _userAccessor.GetUserName());
            
            if(user == null){
                return Unauthorized();
            }
            var photo = user.Photos.FirstOrDefault(x=> x.Id == id);
            if(photo ==null){
                return BadRequest("No such image !!");
            }
            if(photo.IsMain){
                return BadRequest("Cannot delete main photo!!");
            }
            var delete = await _photoAccessor.DeletePhoto(photo.Id);
            
            if(delete ==null){
                return BadRequest("Failed to delete image");
            }
            user.Photos.Remove(photo);
            await _context.SaveChangesAsync();
            return "Image Deleted";
        }

        [HttpPut("update-new-main-photo/{id}")]
        public async Task<ActionResult<Photo>> UpdatePhoto(string id){
            var user = await _context.Users.Include(x=> x.Photos).SingleOrDefaultAsync(x => x.UserName == _userAccessor.GetUserName());
            
            if(user == null){
                return Unauthorized();
            }

            var mainPhoto = user.Photos.FirstOrDefault(x=> x.IsMain ==true);
            mainPhoto.IsMain = false;

            var photo = user.Photos.FirstOrDefault(x=> x.Id == id);
            if(photo ==null){
                return BadRequest("No such image !!");
            }
            photo.IsMain = true;

            await _context.SaveChangesAsync();

            return photo;

        }

    }
}