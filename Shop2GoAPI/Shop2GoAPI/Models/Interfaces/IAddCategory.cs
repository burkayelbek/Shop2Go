using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop2GoAPI.Models.Interfaces
{
    public class IAddCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MatIconName { get; set; }
    }
}
