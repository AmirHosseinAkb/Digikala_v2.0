using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductColorAgg;

namespace ShopManagement.Infrastructure.EfCore.Mappings
{
    public class ProductColorMapping:IEntityTypeConfiguration<ProductColor>
    {
        public void Configure(EntityTypeBuilder<ProductColor> builder)
        {
            builder.ToTable("ProductColors");
            builder.HasKey(c => c.ColorId);
            builder.Property(c=>c.ProductId);
            builder.Property(c=>c.ColorCode);
            builder.Property(c=>c.ColorName);

            builder.HasOne<Product>(c => c.Product).WithMany(p => p.ProductColors)
                .HasForeignKey(c => c.ProductId);
            builder.HasMany<OrderItem>(c=>c.OrderItems).WithOne(i => i.ProductColor).HasForeignKey(i => i.ColorId);
        }
    }
}
