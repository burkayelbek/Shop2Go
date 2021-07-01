using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop2GoAPI.Models
{
    public class Category
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? MatIconName { get; set; }

    }
}
