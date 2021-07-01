using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop2GoAPI.Models.Interfaces
{
    public class IUserMailModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
    }
}
