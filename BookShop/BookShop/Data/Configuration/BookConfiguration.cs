using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Data.Configuration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books");

            builder.HasKey(b => b.Id);

            builder.
                Property(b => b.Name).
                IsRequired();

            builder.
                Property(b => b.Price).
                IsRequired();

            builder.
                HasOne(b => b.Order).
                WithMany(o => o.Books);

            builder.
                HasOne(b => b.Bucket).
                WithMany(b => b.Books).
                HasForeignKey(b => b.BucketId);
        }
    }
}
