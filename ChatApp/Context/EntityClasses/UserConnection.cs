using System.ComponentModel.DataAnnotations;

namespace ChatApp.Context.EntityClasses
{
    public class UserConnection
    {
        [Required]
        public int UserId {  get; set; }
        [Required]
        public string ConnectionId { get; set; }

        
    }
}
