using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shop2GoAPI.Models
{
    public class Conversation
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string ConversationId { get; set; }
        [Required]
        [ForeignKey("UserSenderFK")]
        public string SenderId { get; set; }
        public User UserSenderFK { get; set; }
        [Required]
        public string SenderFullName { get; set; }
        [Required]
        public string RecieverId { get; set; }
        [Required]
        public string RecieverFullName { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public DateTime SentTime { get; set; }
    }
}
