using System;
using API.DTO;

namespace RestAPI_Detailed.DTO
{
    public class ActivityAttendee
    {
        public string AppUserId {get; set;}

        public Guid ActivityId {get; set;}

        public bool IsHost {get; set;}

        public AppUser AppUser {get; set;}

        public Activity Activity {get; set;}
    }
}