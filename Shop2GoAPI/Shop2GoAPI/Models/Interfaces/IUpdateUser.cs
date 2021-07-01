using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop2GoAPI.Models.Interfaces
{
    public class IUpdateUser
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
