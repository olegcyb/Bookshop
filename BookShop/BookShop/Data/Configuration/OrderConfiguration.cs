using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Data.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.
                HasKey(o => o.Id);

            builder
                .Property(o => o.PostAddress)
                .IsRequired();

            builder.
                HasOne(o => o.User).
                WithMany(u=>u.Orders).
                HasForeignKey(u=>u.UserId).
                IsRequired();
        }
    }
}
