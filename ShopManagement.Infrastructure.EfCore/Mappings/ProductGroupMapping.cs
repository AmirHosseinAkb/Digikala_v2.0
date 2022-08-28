using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductGroupAgg;

namespace ShopManagement.Infrastructure.EfCore.Mappings
{
    public class ProductGroupMapping:IEntityTypeConfiguration<ProductGroup>
    {
        public void Configure(EntityTypeBuilder<ProductGroup> builder)
        {
            builder.ToTable("ProductGroups");
            builder.HasKey(g => g.GroupId);
            builder.Property(g => g.GroupTitle).HasMaxLength(300);
            builder.Property(g => g.ParentId);
            builder.Property(g => g.ImageName).IsRequired(false).HasMaxLength(300);

            builder.HasMany(g=>g.ProductGroups).WithOne().HasForeignKey(g => g.ParentId);

            builder.HasMany<Product>(g => g.Products).WithOne(p => p.ProductGroup)
                .HasForeignKey(p => p.GroupId);

            builder.HasMany<Product>(g => g.PrimaryProducts).WithOne(p => p.PrimaryProductGroup)
                .HasForeignKey(p => p.PrimaryGroupId);

            builder.HasMany<Product>(g => g.SecondaryProducts).WithOne(p => p.SecondaryProductGroup)
                .HasForeignKey(p => p.SecondaryGroupId);

            builder.HasMany<GroupDetail>(g => g.GroupDetails).WithOne(d => d.ProductGroup)
                .HasForeignKey(d => d.GroupId);
        }
    }
}
