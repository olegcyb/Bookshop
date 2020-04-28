using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Application;
using BookShop.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Controllers
{
    [Authorize(Roles = "Reader")]
    public class BookController : Controller
    {
        private readonly BookShopDb _context;

        public BookController(BookShopDb context)
        {
            this._context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetListOfBooks(int page=1)
        {
            int pageSize = 8;

            var books = (from i in this._context.Books
                         where (i.BucketId == null) && (i.OrderId == null)
                         select new BookDisplay()
                         {
                             Id = i.Id,
                             Name = i.Name,
                             Author = i.Author,
                             Price = i.Price
                         }).Skip((page - 1) * pageSize).Take(pageSize).ToList(); ;

            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = _context.Books.Count()};

            GetBooksDto dto = new GetBooksDto() { Books=books,PageInfo=pageInfo};

            return View(dto);
        }
    }
}