using BookShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Application
{
    public class GetOrderDto
    {
        public int Id { get; set; }

        public DateTime? date { get; set; }

        public string PostAddress { get; set; }

        public Status Status { get; set; }

        public GetOrderDto(int id,DateTime? date,string address,Status status)
        {
            this.Id = id;
            this.Status = status;
            this.date = date;
            this.PostAddress = address;
        }
    }
}
