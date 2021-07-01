using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop2GoAPI.Models.Interfaces
{
    public class IResetPasswordModel
    {
        public string UserID { get; set; }
        public string Email { get; set; }
        [MinLength(6)]
        public string Password { get; set; }

    }
}
