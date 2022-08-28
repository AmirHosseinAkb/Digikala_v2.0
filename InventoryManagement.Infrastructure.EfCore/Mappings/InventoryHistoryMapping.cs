using InventoryManagement.Domain.InventoryHistoryAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Infrastructure.EfCore.Mappings
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
            builder.Property(h=>h.Date);
        }
    }
}
