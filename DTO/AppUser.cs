

using Microsoft.AspNetCore.Identity;

namespace RestAPI_Detailed.DTO
{
    public class AppUser : IdentityUser
    {
        public string DispalyName {get; set;}
        public string Bio {get;set;}


    }
}