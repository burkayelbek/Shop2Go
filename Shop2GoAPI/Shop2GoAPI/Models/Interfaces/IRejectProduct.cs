using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop2GoAPI.Models.Interfaces
{
    public class IRejectProduct
    {
        public int Id { get; set; }
        public string RejectedMessage { get; set; }

    }
}
