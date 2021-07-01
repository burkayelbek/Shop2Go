using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shop2GoAPI.Models
{
    public class RestrictedUsers
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [ForeignKey("User")]
        public string UserID { get; set; }
        public User User { get; set; }
        [Required]
        public DateTime RestrictedDate { get; set; }
    }
}
