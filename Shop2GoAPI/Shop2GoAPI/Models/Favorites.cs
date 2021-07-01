using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shop2GoAPI.Models
{
    public class Favorites
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public string UserID { get; set; }
        public User User { get; set; }

        [ForeignKey("Products")]
        public int ProductId { get; set; }
        public Products Products { get; set; }
    }
}
