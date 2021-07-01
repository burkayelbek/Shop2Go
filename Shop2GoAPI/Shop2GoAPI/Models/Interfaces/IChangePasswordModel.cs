using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop2GoAPI.Models.Interfaces
{
    public class IChangePasswordModel
    {
        public string CurrentPassword { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
