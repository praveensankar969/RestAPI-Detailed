using System;
using System.Collections.Generic;
using RestAPI_Detailed.DTO;

namespace API.DTO
{
    public class ActivityDTO
    {
         public Guid Id {get; set;}
        public string Description {get;set;}
        public DateTime? Date {get;set;}
        public string Title {get;set;}
        public string Category {get; set;}
        public string City {get; set;}
        public string Venue {get; set;}

        public string HostName {get; set;}
        public bool IsCancelled {get; set;}

        public ICollection<AttendeeDTO> Attendees {get; set;}

    }
}