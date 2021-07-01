using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shop2GoAPI.Models
{
    public class Products
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public float Price { get; set; }
        public string? Description { get; set; }
        [Required]
        public DateTime PublishedDate { get; set; }

        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        [Required]
        public Category Category { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserID { get; set; }
        public User User { get; set; }
        [Required]
        public int isApproved { get; set; }
        public string? RejectedMessage { get; set; }
        [Required]
        public int isSold { get; set; }
        public string? BuyerUserName { get; set; }

        //FK
        public int ReviewId { get; set; }
    }
}
