using DiscountManagement.Domain.OrderDiscountAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscountManagement.Infrastructure.EfCore.Mappings;

public class OrderDiscountMapping:IEntityTypeConfiguration<OrderDiscount>
{
    public void Configure(EntityTypeBuilder<OrderDiscount> builder)
    {
        builder.ToTable("OrderDiscounts");
        builder.HasKey(d => d.DiscountId);
        builder.Property(d => d.DiscountCode).HasMaxLength(200);
        builder.Property(d => d.DiscountRate);
        builder.Property(d => d.UsableCount).IsRequired(false);
        builder.Property(d => d.StartDate).IsRequired(false);
        builder.Property(d => d.EndDate).IsRequired(false);
        builder.Property(d => d.Reason).HasMaxLength(200);
        builder.Property(d => d.IsDeleted);

        builder.HasQueryFilter(d => !d.IsDeleted);
    }
}