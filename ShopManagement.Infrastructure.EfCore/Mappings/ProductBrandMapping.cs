using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductBrandAgg;
using ShopManagement.Domain.ProductGroupAgg;

namespace ShopManagement.Infrastructure.EfCore.Mappings
{
    public class ProductBrandMapping:IEntityTypeConfiguration<ProductBrand>
    {
        public void Configure(EntityTypeBuilder<ProductBrand> builder)
        {
            builder.ToTable("ProductBrands");
            builder.HasKey(b => b.BrandId);
            builder.Property(b => b.GroupId);
            builder.Property(b => b.BrandTitle).HasMaxLength(200);
            builder.Property(b => b.OtherLangTitle).HasMaxLength(200);

            builder.HasMany<Product>(b => b.Products).WithOne(p => p.ProductBrand)
                .HasForeignKey(p => p.BrandId);

            builder.HasOne<ProductGroup>(b => b.ProductGroup).WithMany(g => g.ProductBrands)
                .HasForeignKey(b => b.GroupId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
