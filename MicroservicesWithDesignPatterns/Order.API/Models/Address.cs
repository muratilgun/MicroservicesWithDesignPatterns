using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.API.Models
{
    
    public class Address
    {
        public string Line { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
    }
}
