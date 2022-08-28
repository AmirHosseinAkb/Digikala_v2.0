using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.InventoryAgg;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Infrastructure.EfCore.Mappings
{
    public class InventoryHistoryMapping:IEntityTypeConfiguration<InventoryHistory>
    {
        public void Configure(EntityTypeBuilder<InventoryHistory> builder)
        {
            builder.ToTable("InventoryHistories");
            builder.HasKey(h => h.InventoryHistoryId);
            builder.Property(h => h.ProductId);
            builder.Property(h => h.OperatorId);
            builder.Property(h => h.Count);
            builder.Property(h => h.Date);


            builder.HasOne<Product>(h => h.Product).WithMany(p => p.InventoryHistories)
                .HasForeignKey(h => h.ProductId);
        }
    }
}
