

using System.Collections.Generic;
using API.DTO;
using Microsoft.AspNetCore.Identity;

namespace RestAPI_Detailed.DTO
{
    public class AppUser : IdentityUser
    {
        public string DispalyName {get; set;}
        public string Bio {get;set;}
        public ICollection<ActivityAttendee> Activities {get; set;}

    }
}