using System;
using API.DTO;

namespace RestAPI_Detailed.DTO
{
    public class Comment
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public AppUser Author { get; set; }
        public Activity Activity {get; set;}
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}