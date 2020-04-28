using BookShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Application
{
    public class GetOrderDetailsDto
    {
        public string Email { get; set; }

        public double TotalSum { get; set; }

        public List<BookDisplay> Books { get; set; } = new List<BookDisplay>();
    }
}
