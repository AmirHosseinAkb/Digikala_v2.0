using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.InventoryAgg;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductBrandAgg;
using ShopManagement.Domain.ProductColorAgg;
using ShopManagement.Domain.ProductGroupAgg;
using ShopManagement.Domain.ProductImageAgg;
using ShopManagement.Infrastructure.EfCore.Mappings;

namespace ShopManagement.Infrastructure.EfCore
{
    public class ShopContext:DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<GroupDetail> GroupDetails { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<InventoryHistory> InventoryHistories { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductMapping());
            modelBuilder.ApplyConfiguration(new ProductGroupMapping());
            modelBuilder.ApplyConfiguration(new ProductImageMapping());
            modelBuilder.ApplyConfiguration(new ProductColorMapping());
            modelBuilder.ApplyConfiguration(new GroupDetailMapping());
            modelBuilder.ApplyConfiguration(new ProductDetailMapping());
            modelBuilder.ApplyConfiguration(new InventoryMapping());
            modelBuilder.ApplyConfiguration(new InventoryHistoryMapping());
            modelBuilder.ApplyConfiguration(new ProductBrandMapping());
            base.OnModelCreating(modelBuilder);
        }
    }
}
