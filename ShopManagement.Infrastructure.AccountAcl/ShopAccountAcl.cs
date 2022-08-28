using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopManagement.Domain.Services;
using UserManagement.Application.Contracts.User;

namespace ShopManagement.Infrastructure.AccountAcl
{
    public class ShopAccountAcl : IShopAccountAcl
    {
        private readonly IUserApplication _userApplication;

        public ShopAccountAcl(IUserApplication userApplication)
        {
            _userApplication = userApplication;
        }
        public (string fullName, string roleTitle) GetUser(long userId)
        {
            var user = _userApplication.GetUserBy(userId);
            return (user.FullName, user.RoleTitle);
        }

    }
}

