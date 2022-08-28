using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductImageAgg;

namespace ShopManagement.Infrastructure.EfCore.Mappings
{
    public class ProductImageMapping:IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImages");
            builder.HasKey(i => i.ImageId);
            builder.Property(i => i.ImageName);
            builder.Property(i => i.ProductId);

            builder.HasOne<Product>(i => i.Product).WithMany(p => p.ProductImages)
                .HasForeignKey(i => i.ProductId);

        }
    }
}
