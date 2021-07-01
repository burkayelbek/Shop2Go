using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop2GoAPI.Models.Interfaces
{
    public class IUserTicketModel
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public int Priority { get; set; }
    }
}
