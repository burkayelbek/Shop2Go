using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop2GoAPI.Models.Interfaces
{
    public class IMessageModel
    {
        public string RecieverId { get; set; }
        public string Message { get; set; }
    }
}
