using ChatApp.Business.Helpers;
using System.ComponentModel.DataAnnotations;

namespace ChatApp.Models
{
    public class ChatViewModel
    {
        [Required]
        public int SenderId { get; set; }
        [Required]
        public int RecieverId { get; set; }
        [Required]
        public ChatType? ChatType { get; set; }
        [Required]
        public string ChatContent { get; set; }
        public int? RepliedTo { get; set; }
    }
}
