using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.InventoryAgg;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductBrandAgg;
using ShopManagement.Domain.ProductColorAgg;
using ShopManagement.Domain.ProductGroupAgg;
using ShopManagement.Domain.ProductImageAgg;

namespace ShopManagement.Infrastructure.EfCore.Mappings
{
    public class ProductMapping:IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(p => p.ProductId);
            builder.Property(p => p.GroupId);
            builder.Property(p => p.PrimaryGroupId).IsRequired(false);
            builder.Property(p => p.SecondaryGroupId).IsRequired(false);
            builder.Property(p => p.Title).HasMaxLength(300);
            builder.Property(p => p.OtherLangTitle).HasMaxLength(300);
            builder.Property(p => p.Description);
            builder.Property(p => p.Price);
            builder.Property(p => p.Tags).HasMaxLength(300);
            builder.Property(p => p.ImageName).HasMaxLength(300);
            builder.Property(p => p.CreationDate);
            builder.Property(p => p.IsDeleted);

            builder.HasOne<ProductGroup>(p => p.ProductGroup)
                .WithMany(g => g.Products).HasForeignKey(p => p.GroupId);

            builder.HasOne<ProductGroup>(p => p.PrimaryProductGroup).WithMany(g => g.PrimaryProducts)
                .HasForeignKey(p => p.PrimaryGroupId);

            builder.HasOne<ProductGroup>(p => p.SecondaryProductGroup).WithMany(g => g.SecondaryProducts)
                .HasForeignKey(p => p.SecondaryGroupId);

            builder.HasMany<ProductColor>(p => p.ProductColors).WithOne(c => c.Product)
                .HasForeignKey(c => c.ProductId);

            builder.HasMany<ProductImage>(p => p.ProductImages).WithOne(i => i.Product)
                .HasForeignKey(i => i.ProductId);
             
            builder.HasMany<ProductDetail>(p => p.ProductDetails).WithOne(d => d.Product)
                .HasForeignKey(d => d.ProductId);

            builder.HasOne(p => p.Inventory).WithOne(i => i.Product)
                .HasForeignKey<Inventory>(i => i.ProductId);

            builder.HasMany<InventoryHistory>(p => p.InventoryHistories).WithOne(h => h.Product)
                .HasForeignKey(h => h.ProductId);

            builder.HasOne<ProductBrand>(p => p.ProductBrand).WithMany(b => b.Products)
                .HasForeignKey(p => p.BrandId);

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
