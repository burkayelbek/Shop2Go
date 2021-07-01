using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop2GoAPI.Models.Interfaces
{
    public class IUpdateTicketModel
    {
        public string Id { get; set; }
        public int Status { get; set; }
        public string FeedbackMessage { get; set; }
    }
}
