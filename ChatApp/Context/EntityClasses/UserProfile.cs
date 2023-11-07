using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Context.EntityClasses
{
    public class UserProfile
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string PhoneNumber {  get; set; }
        public DateTime DateOfBirth { get; set; }
        public required string Gender { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageName { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime LastUpdatedAt { get; set; }

        // Relationships

        /*[InverseProperty(nameof(Chat.SenderProfile))]
        public ICollection<Chat> SenderChats { get; set; }

        [InverseProperty(nameof(Chat.RecieverProfile))]
        public ICollection<Chat> ReciverChats { get; set; }*/


    }
}
