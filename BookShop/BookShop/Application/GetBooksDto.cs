using BookShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Application
{
    public class GetBooksDto
    {
        public List<BookDisplay> Books { get; set; }

        public PageInfo PageInfo { get; set; }
    }
}
