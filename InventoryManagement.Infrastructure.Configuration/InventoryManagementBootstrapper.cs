using InventoryManagement.Application;
using InventoryManagement.Application.Contracts.Inventory;
using InventoryManagement.Application.Contracts.InventoryHistory;
using InventoryManagement.Domain.InventoryAgg;
using InventoryManagement.Domain.InventoryHistoryAgg;
using InventoryManagement.Infrastructure.EfCore;
using InventoryManagement.Infrastructure.EfCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagement.Infrastructure.Configuration
{
    public class InventoryManagementBootstrapper
    {
        public static void Config(IServiceCollection services, string connectionString)
        {
            services.AddScoped<IInventoryRepository, IInventoryRepository>();
            services.AddScoped<IInventoryApplication, IInventoryApplication>();
            services.AddScoped<IInventoryHistoryRepository,InventoryHistoryRepository>();
            services.AddScoped<IInventoryHistoryApplication, InventoryHistoryApplication>();
            services.AddDbContext<InventoryContext>(options =>
                options.UseSqlServer(connectionString));
        }
    }
}
