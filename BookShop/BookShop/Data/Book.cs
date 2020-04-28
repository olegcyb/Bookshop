using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Data
{
    public class Book
    {
        public int Id { get; set; }

        public string  Name { get; set; }

        public string Author { get; set; }

        public string Janre { get; set; }

        public int Year { get; set; }

        public double Price { get; set; }

        public int? BucketId { get; set; }
        public Bucket Bucket { get; set; }

        public int? OrderId { get; set; }
        public Order Order { get; set; }
    }
}
