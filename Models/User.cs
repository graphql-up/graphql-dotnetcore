using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Market.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
