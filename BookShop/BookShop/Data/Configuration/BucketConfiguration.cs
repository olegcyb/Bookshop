using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Data.Configuration
{
    public class BucketConfiguration : IEntityTypeConfiguration<Bucket>
    {
        public void Configure(EntityTypeBuilder<Bucket> builder)
        {
            builder.HasKey(b => b.Id);
        
            builder.ToTable("Buckets");

            builder.
                HasOne(b => b.User).
                WithOne(u=>u.Bucket).
                IsRequired();

            builder.
                HasMany(b => b.Books).
                WithOne(b => b.Bucket).
                HasForeignKey(b => b.BucketId);
        }
    }
}
