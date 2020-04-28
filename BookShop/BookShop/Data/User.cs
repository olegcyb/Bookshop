using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Data
{ //User's plate
    public class User:IdentityUser
    {
        public int? Age { get; set; }

        public string  City { get; set; }
        public string Street { get; set; }

        public Bucket Bucket { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();

    }
}
