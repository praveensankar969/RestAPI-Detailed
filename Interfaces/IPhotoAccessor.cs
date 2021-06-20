using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RestAPI_Detailed.DTO;

namespace RestAPI_Detailed.Interfaces
{
    public interface IPhotoAccessor
    {
         Task<PhotoDTO> UploadPhoto(IFormFile file);
         Task<string> DeletePhoto(string photoId);
         
    }
}