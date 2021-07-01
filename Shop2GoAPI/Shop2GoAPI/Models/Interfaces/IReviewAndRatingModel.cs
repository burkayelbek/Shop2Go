using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop2GoAPI.Models.Interfaces
{
    public class IReviewAndRatingModel
    {
        public string RecieverId { get; set; }
        public int StarRating { get; set; }
        public string Comment { get; set; }
    }
}
