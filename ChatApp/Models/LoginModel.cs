using System.ComponentModel.DataAnnotations;

namespace ChatApp.Models
{
    public class LoginModel
    {
        [Required]
        public required string Username { get; set; }
        [EmailAddress]
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }

    }
}
