using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.
                HasOne(o => o.Bucket).
                WithOne(b => b.User).
                IsRequired();

            builder.
                HasMany(u => u.Orders).
                WithOne(o => o.User).
                HasForeignKey(o => o.UserId);
        }
    }
}
