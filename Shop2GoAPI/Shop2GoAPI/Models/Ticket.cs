using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shop2GoAPI.Models
{
    public class Ticket
    {
        [Required]
        public string Id { get; set; }
        [Required]
        [ForeignKey("UserFK")]
        public string UserID { get; set; }
        public User UserFK { get; set; }
        [Required]
        public string UserFullName { get; set; }
        [Required]
        public string Title { get; set; }
        public string Message { get; set; }
        [Required]
        public int Priority { get; set; }
        [Required]
        public int Status { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        public DateTime? FixedDate { get; set; }
        public string? FeedbackMessage { get; set; }

    }
}
