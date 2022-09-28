using DiscountManagement.Domain.ProductDiscountAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscountManagement.Infrastructure.EfCore.Mappings
{
    public class ProductDiscountMapping:IEntityTypeConfiguration<ProductDiscount>
    {
        public void Configure(EntityTypeBuilder<ProductDiscount> builder)
        {
            builder.ToTable("ProductDiscounts");
            builder.HasKey(d => d.ProductDiscountId);
            builder.Property(d => d.ProductId);
            builder.Property(d => d.Rate);
            builder.Property(d => d.StartDate);
            builder.Property(d => d.EndDate);
            builder.Property(d => d.IsDeleted);

            builder.HasQueryFilter(d => !d.IsDeleted);
        }
    }
}
