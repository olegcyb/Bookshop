using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Data
{
    public enum Status
    {
        Opened,
        Closed
    }

    public class Order
    {
        public int Id { get; set; }

        public string PostAddress { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public DateTime? date { get; set; }

        public Status Status { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
