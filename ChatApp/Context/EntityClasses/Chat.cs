using ChatApp.Business.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Context.EntityClasses
{
    public class Chat
    {
        public int Id { get; set; }
        
        public ChatType? ChatType { get; set; }
        
        [Required]
        public string ChatContent { get; set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }
        
        [Required, DataType(DataType.DateTime)]
        public DateTime UpdatedAt { get; set; }
        public Boolean IsDeletedBySender { get; set; }
        public Boolean IsDeletedByReciever { get; set; }

        // Relationships

        [ForeignKey(nameof(SenderProfile))]
        public int SenderId { get; set; }
        public UserProfile SenderProfile { get; set; }

        [ForeignKey(nameof(RecieverProfile))]
        public int RecieverId { get; set; }
        public UserProfile RecieverProfile { get; set; }

        [ForeignKey(nameof(Id))]
        public int? RepliedTo { get; set; }
    }
}
