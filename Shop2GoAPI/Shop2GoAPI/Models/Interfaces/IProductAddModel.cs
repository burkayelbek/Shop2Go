using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop2GoAPI.Models.Interfaces
{
    public class IProductAddModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public List<byte[]> Image { get; set; }
    }
}
