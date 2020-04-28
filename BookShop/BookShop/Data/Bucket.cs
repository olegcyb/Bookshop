using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Data
{
    public class Bucket
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
