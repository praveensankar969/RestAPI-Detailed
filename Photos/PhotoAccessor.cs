using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using RestAPI_Detailed.DTO;
using RestAPI_Detailed.Interfaces;

namespace RestAPI_Detailed.Photos
{
    public class PhotoAccessor : IPhotoAccessor
    {
        private readonly Cloudinary cloudinary;
        public PhotoAccessor(IOptions<CloudinarySettings> config)
        {
            var account = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );
            cloudinary = new Cloudinary(account);
        }

        public async Task<string> DeletePhoto(string photoId)
        {
            var deleteParams = new DeletionParams(photoId);
            var result = await cloudinary.DestroyAsync(deleteParams);
            return (result.Result =="ok" )? result.Result : null;
        }

        public async Task<PhotoDTO> UploadPhoto(IFormFile file)
        {
           
            if(file.Length>0){
                await using var stream = file.OpenReadStream();
                var photoParams = new ImageUploadParams{
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(400).Width(400).Crop("fill")
                };
                var upload = await cloudinary.UploadAsync(photoParams);

                if(upload.Error !=null){
                    throw new System.Exception(upload.Error.Message);
                }

                return new PhotoDTO{
                    PhotoUrl = upload.SecureUrl.ToString(),
                    PhotoId = upload.PublicId
                };
            }

            return null;
        }
    }
} 