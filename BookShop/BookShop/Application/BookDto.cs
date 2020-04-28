using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Application
{
    public class BookDto
    {
        public string Name { get; set; }

        public string Author { get; set; }

        public string Janre { get; set; }

        public double Price { get; set; }

        public int Year { get; set; }
    }
}
