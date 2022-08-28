using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.InventoryAgg;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Infrastructure.EfCore.Mappings
{
    public class InventoryMapping:IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.ToTable("Inventories");
            builder.HasKey(i => i.InventoryId);
            builder.Property(i => i.ProductId);
            builder.Property(i => i.ProductCount);

            builder.HasOne<Product>(i => i.Product).WithOne(p => p.Inventory)
                .HasForeignKey<Inventory>(i => i.ProductId);
        }
    }
}
