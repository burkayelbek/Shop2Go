using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shop2GoAPI.Models
{
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ProductFK")]
        public int ProductId { get; set; }
        public Products ProductFK { get; set; }
        public byte[] Image { get; set; }
    }
}
