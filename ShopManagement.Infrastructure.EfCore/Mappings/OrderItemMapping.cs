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
    public class OrderItemMapping:IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");
            builder.HasKey(i => i.OrderItemId);
            builder.Property(i=>i.OrderId);
            builder.Property(i=>i.ProductId);
            builder.Property(i=>i.ColorId);
            builder.Property(i=>i.UnitPrice);
            builder.Property(i=>i.Count);

            builder.HasOne<Product>(i=>i.Product).WithMany(p => p.OrderItems).HasForeignKey(i => i.ProductId);
            builder.HasOne<Order>(i=>i.Order).WithMany(o => o.OrderItems).HasForeignKey(i => i.OrderId);
            builder.HasOne<ProductColor>(i=>i.ProductColor).WithMany(c => c.OrderItems).HasForeignKey(i => i.ColorId);
        }
    }
}
