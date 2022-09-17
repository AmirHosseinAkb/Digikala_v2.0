using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscountManagement.Application;
using DiscountManagement.Application.Contracts.OrderDiscount;
using DiscountManagement.Domain.OrderDiscountAgg;
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

            services.AddDbContext<DiscountContext>(options =>
                options.UseSqlServer(connectionString));
        }
    }
}
