using System;
using System.Collections.Generic;
using System.Text;
using BookShop.Data.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Data
{
    public class BookShopDb : IdentityDbContext
    {
        public DbSet<Book> Books { get; set; }

        public DbSet<Bucket> Buckets { get; set; }

        public DbSet<Order> Orders { get; set; }

        public BookShopDb(DbContextOptions<BookShopDb> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new BookConfiguration());

            builder.ApplyConfiguration(new UserConfiguration());

            builder.ApplyConfiguration(new OrderConfiguration());

            builder.ApplyConfiguration(new BucketConfiguration());
        }
    }
}
