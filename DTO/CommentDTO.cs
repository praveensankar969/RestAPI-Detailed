using System;

namespace RestAPI_Detailed.DTO
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Body { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Image { get; set; }

    }
}