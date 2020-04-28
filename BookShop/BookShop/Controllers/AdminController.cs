using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Application;
using BookShop.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly BookShopDb _context;
        private readonly UserManager<IdentityUser> _manager;

        public AdminController(BookShopDb context,UserManager<IdentityUser> manager)
        {
            this._context = context;
            this._manager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {

            var bookToDelete = this._context.Books.Find(id);

            this._context.Books.Remove(bookToDelete);

            await this._context.SaveChangesAsync();

            return RedirectToAction("GetListOfBooks", "Book");
        }

        [HttpGet]
        public IActionResult AddBook()
        {
            return View("AddBook");
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(BookDto newbook)
        {
            Book book = new Book() { Name = newbook.Name,
                Author = newbook.Author,
                Janre = newbook.Janre,
                Year = newbook.Year ,Price=newbook.Price};

            await this._context.Books.AddAsync(book);

            await this._context.SaveChangesAsync();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Book book= await this._context.Books.FindAsync(id);
            BookEdit edit = new BookEdit()
            {
                Id = id,
                Name = book.Name,
                Author = book.Author,
                Janre = book.Janre,
                Price = book.Price,
                Year = book.Year
            };

            return View(edit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BookEdit bookedit)
        {
            Book book = await this._context.Books.FindAsync(bookedit.Id);

            book.Name = bookedit.Name;

            book.Janre = bookedit.Janre;

            book.Author = bookedit.Author;

            book.Price = bookedit.Price;

            await this._context.SaveChangesAsync();

            return RedirectToAction("GetListOfBooks", "Book");
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            var orders = (from o in this._context.Orders
                          orderby o.Status
                         select new GetOrderDto(o.Id,o.date,o.PostAddress,o.Status)).
                         ToList<GetOrderDto>();
                         
            return View(orders);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderDetails(int id)
        {
            Order order = await this._context.Orders.FindAsync(id);

            User user = (User)await this._manager.FindByIdAsync(order.UserId);

            GetOrderDetailsDto dto = new GetOrderDetailsDto()
            {
                Email = user.Email
            };

            var books = (from i in this._context.Books
                        where i.OrderId == order.Id
                        select new BookDisplay()
                        {
                            Id = i.Id,
                            Name = i.Name,
                            Author = i.Author,
                            Price = i.Price
                        }).ToList<BookDisplay>();

            double totalsum = books.Sum(b => b.Price);

            dto.Books = books;
            dto.TotalSum = totalsum;

            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> OpenOrder(int id)
        {
            Order order =await this._context.Orders.FindAsync(id);

            order.Status = Status.Opened;

            await this._context.SaveChangesAsync();

            return RedirectToAction("GetOrders", "Admin");
        }

        [HttpGet]
        public async Task<IActionResult> CloseOrder(int id)
        {
            Order order = await this._context.Orders.FindAsync(id);

            order.Status = Status.Closed;

            await this._context.SaveChangesAsync();

            return RedirectToAction("GetOrders", "Admin");
        }
    }
}