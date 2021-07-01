using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop2GoAPI.Models.Interfaces
{
    public class IUpdateProductSoldStatus
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string UserName { get; set; }
    }
}
