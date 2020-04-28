using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Application;
using BookShop.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Controllers
{
    [Authorize(Roles ="Reader")]
    public class UserController : Controller
    {
        private readonly BookShopDb _context;
        private readonly UserManager<IdentityUser> _manager;

        public UserController(BookShopDb context,UserManager<IdentityUser> manager)
        {
            this._context = context;
            this._manager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> ProfileInfo()
        {
            User user = (User)await this._manager.FindByNameAsync(HttpContext.User.Identity.Name);

            ProfileDto profile = new ProfileDto()
            {
                Email = user.Email,
                Street = user.Street,
                Age = user.Age,
                City = user.City,
            };

            var orders = (from o in this._context.Orders
                             where o.UserId.Equals(user.Id)
                             join book in this._context.Books on o.Id equals book.OrderId
                          select new {o,book}).ToList();

            int countbooks = orders.Count;
            double totalsum = orders.Sum(o => o.book.Price);

            UserStatistic statistic = new UserStatistic()
            {
                TotalSum = totalsum,
                CountBooks = countbooks,
                CountOrders = user.Orders.Count
            };

            ViewData["statistic"] = statistic;

            return View(profile);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(ProfileDto profile)
        {
            User user = (User)await this._manager.FindByNameAsync(HttpContext.User.Identity.Name);

            user.Age = profile.Age;
            user.City = profile.City;
            user.Street = profile.Street;

            await this._context.SaveChangesAsync();

            return RedirectToAction("ProfileInfo", "User");
        }

        [HttpGet]
        public async Task<IActionResult> GetBucketInfo()
        {
            User requesteduser = (User) await this._manager.FindByNameAsync(HttpContext.User.Identity.Name);

            Bucket bucket = (from i in this._context.Buckets
                             where i.UserId.Equals(requesteduser.Id)
                             select i).ToList<Bucket>().First();

            var books = from i in this._context.Books
                        where i.BucketId==bucket.Id
                        select new BookDisplay() { Name = i.Name, Author = i.Author, Price = i.Price ,Id=i.Id };

            double sum = books.Sum(b => b.Price);

            BucketDisplay bucdis = new BucketDisplay() { TotalPrice = sum,Books=books.ToList() };

            return View(bucdis);
        }

        [HttpGet]
        public async Task<IActionResult> AddToBucket(int id)
        {
            Book book= await this._context.Books.FindAsync(id);

            User requesteduser= (User)await this._manager.FindByNameAsync(HttpContext.User.Identity.Name);

            Bucket bucket = (from i in this._context.Buckets
                            where i.UserId.Equals(requesteduser.Id)
                            select i).ToList<Bucket>().First();

            book.Bucket = requesteduser.Bucket;

            book.BucketId = requesteduser.Bucket.Id;

            bucket.Books.Add(book);

            await this._context.SaveChangesAsync();

            return RedirectToAction("GetListOfBooks", "Book");
        }

        [HttpGet]
        public async Task<IActionResult> CancelBook(int id)
        {
            Book book = await this._context.Books.FindAsync(id);
            book.BucketId = null;

            await this._context.SaveChangesAsync();

            return RedirectToAction("GetBucketInfo", "User");
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDto dto)
        {
            User user = (User)await this._manager.FindByNameAsync(HttpContext.User.Identity.Name);

            Order order = new Order() {
                PostAddress = dto.Address,
                date = DateTime.Now,
                Status = Status.Opened,
                UserId=user.Id,
                User=user
            };

            await this._context.SaveChangesAsync();

            Bucket bucket = (from i in this._context.Buckets
                             where i.UserId.Equals(user.Id)
                             select i).ToList<Bucket>().First();

            var books = (from i in this._context.Books
                         where i.BucketId == bucket.Id
                         select i).ToList<Book>();

            books.ForEach(b =>
            {
                b.OrderId = order.Id;
                b.Order = order;
                b.BucketId = null;
                b.Bucket = null;
            });

            await this._context.SaveChangesAsync();

            return RedirectToAction("GetBucketInfo", "User");
        }

        [HttpGet]
        public async Task<IActionResult> History()
        {
            User user = (User)await this._manager.FindByNameAsync(HttpContext.User.Identity.Name);

            var orders = (from i in this._context.Orders
                         where i.UserId == user.Id
                         select new GetOrderDto(i.Id, i.date, i.PostAddress, i.Status)).ToList();
  
            return View(orders);
        }
    }
}