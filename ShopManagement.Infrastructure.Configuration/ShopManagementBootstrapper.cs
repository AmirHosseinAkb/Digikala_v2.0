using _01_Framework.Infrastructure;
using DigikalaQuery.Contracts.ProductGroup;
using DigikalaQuery.Queries;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application;
using ShopManagement.Application.Contracts.Inventory;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductColor;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastructure.EfCore;
using ShopManagement.Infrastructure.EfCore.Repositories;
using ShopManagement.Application.Contracts.ProductGroup;
using ShopManagement.Application.Contracts.ProductImage;
using ShopManagement.Domain.InventoryAgg;
using ShopManagement.Domain.ProductColorAgg;
using ShopManagement.Domain.ProductGroupAgg;
using ShopManagement.Domain.ProductImageAgg;
using ShopManagement.Domain.Services;
using ShopManagement.Infrastructure.AccountAcl;
using ShopManagement.Infrastructure.Configuration.Permissions;

namespace ShopManagement.Infrastructure.Configuration
{
    public class ShopManagementBootstrapper
    {
        public static void Config(IServiceCollection services,string connectionString)
        {
            services.AddScoped<IProductRepository,ProductRepository>();
            services.AddScoped<IProductApplication,ProductApplication>();
            services.AddScoped<IProductGroupApplication,ProductGroupApplication>();
            services.AddScoped<IProductGroupRepository,ProductGroupRepository>();
            services.AddScoped<IProductColorApplication, ProductColorApplication>();
            services.AddScoped<IProductColorRepository, ProductColorRepository>();
            services.AddScoped<IProductImageRepository, ProductImageRepository>();
            services.AddScoped<IProductImageApplication, ProductImageApplication>();
            services.AddScoped<IInventoryRepository, InventoryRepository>();
            services.AddScoped<IInventoryApplication, InventoryApplication>();
            services.AddScoped<IPermissionExposer, ProductPermissionExposer>();
            services.AddScoped<IPermissionExposer, InventoryPermissionExposer>();
            services.AddScoped<IPermissionExposer, ProductGroupPermissionExposer>();
            services.AddScoped<IShopAccountAcl, ShopAccountAcl>();

            services.AddScoped<IProductGroupQuery,ProductGroupQuery>();

            services.AddDbContext<ShopContext>(options => 
                options.UseSqlServer(connectionString));
        }
    }
}
