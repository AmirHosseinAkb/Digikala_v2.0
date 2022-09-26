using DiscountManagement.Application;
using DiscountManagement.Application.Contracts.OrderDiscount;
using DiscountManagement.Application.Contracts.ProductDiscount;
using DiscountManagement.Domain.OrderDiscountAgg;
using DiscountManagement.Domain.ProductDiscountAgg;
using DiscountManagement.Infrastructure.EfCore;
using DiscountManagement.Infrastructure.EfCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DiscountManagement.Infrastructure.Configuration
{
    public class DiscountManagentBootstrapper
    {
        public static void Config(IServiceCollection services, string connectionString)
        {
            services.AddScoped<IOrderDiscountRepository, OrderDiscountRepository>();
            services.AddScoped<IOrderDiscountApplication, OrderDiscountApplication>();
            services.AddScoped<IProductDiscountRepository, ProductDiscountRepository>();
            services.AddScoped<IProductDiscountApplication, ProductDiscountApplication>();

            services.AddDbContext<DiscountContext>(options =>
                options.UseSqlServer(connectionString));
        }
    }
}
