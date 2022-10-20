using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.OrderAgg;

namespace ShopManagement.Infrastructure.EfCore.Mappings;

public class OrderMapping:IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(o=>o.OrderId);
        builder.Property(o => o.UserId);
        builder.Property(o => o.DiscountId).IsRequired(false);
        builder.Property(o => o.AddressId).IsRequired(false);
        builder.Property(o => o.OrderSum);
        builder.Property(o => o.OrderDiscount);
        builder.Property(o => o.PaidPrice);
        builder.Property(o => o.TrackingNumber).HasMaxLength(200);
        builder.Property(o => o.PaymentType);
        builder.Property(o => o.Status);
        builder.Property(o => o.IsClosed);
        builder.Property(o => o.CreationDate);
        builder.Property(o => o.IsDeleted);

        builder.HasMany<OrderItem>(o => o.OrderItems).WithOne(i => i.Order)
            .HasForeignKey(i => i.OrderId);
    }
}