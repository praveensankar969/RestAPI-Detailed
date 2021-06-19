using System.ComponentModel.DataAnnotations;

namespace RestAPI_Detailed.DTO
{
    public class RegisterDTO
    {
        [Required]
        public string DisplayName {get;set;}
        [Required]
        [EmailAddress]
        public string Email {get; set;}
        [Required]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[a-zA-Z\\d]{8,}$", ErrorMessage ="Password must contain minimum eight characters, at least one uppercase letter, one lowercase letter and one number")]
        public string Password {get; set;}
        [Required]
        public string UserName {get; set;}
    }
}