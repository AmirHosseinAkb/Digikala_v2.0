using _01_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.Application;
using UserManagement.Application.Contracts.Role;
using UserManagement.Application.Contracts.Transaction;
using UserManagement.Application.Contracts.User;
using UserManagement.Domain.RoleAgg;
using UserManagement.Domain.TransactionAgg;
using UserManagement.Domain.UserAgg;
using UserManagement.Infrastructure.Configuration.Permissions;
using UserManagement.Infrastructure.EfCore;
using UserManagement.Infrastructure.EfCore.Repositories;

namespace UserManagement.Infrastructure.Configuration
{
    public class UserManagementBootstrapper
    {
        public static void Config(IServiceCollection services,string connectionString)
        {
            services.AddScoped<IUserRepository,UserRepository>();
            services.AddScoped<IUserApplication,UserApplication>();
            services.AddScoped<IRoleRepository,RoleRepository>();
            services.AddScoped<IRoleApplication,RoleApplication>();
            services.AddScoped<ITransactionRepository,TransactionRepository>();
            services.AddScoped<ITransactionApplication,TransactionApplication>();
            services.AddTransient<IPermissionExposer,UserPermissionExposer>();
            services.AddTransient<IPermissionExposer,RolePermissionExposer>();
            services.AddDbContext<AccountContext>(options =>
                options.UseSqlServer(connectionString));
        }
    }
}
