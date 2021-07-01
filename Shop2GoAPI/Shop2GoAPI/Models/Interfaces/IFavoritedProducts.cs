using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop2GoAPI.Models.Interfaces
{
    public class IFavoritedProducts
    {
        public int Id { get; set; }
        public string UserID { get; set; }
        public int ProductId { get; set; }
        public string Title { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public DateTime PublishedDate { get; set; }
    }
}
