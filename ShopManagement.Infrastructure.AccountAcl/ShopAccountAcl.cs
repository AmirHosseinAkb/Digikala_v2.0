using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Resources;
using ShopManagement.Domain.Services;
using UserManagement.Application.Contracts.Address;
using UserManagement.Application.Contracts.User;

namespace ShopManagement.Infrastructure.AccountAcl
{
    public class ShopAccountAcl : IShopAccountAcl
    {
        private readonly IUserApplication _userApplication;
        private readonly IAddressApplication _addressApplication;

        public ShopAccountAcl(IUserApplication userApplication, IAddressApplication addressApplication)
        {
            _userApplication = userApplication;
            _addressApplication = addressApplication;
        }
        public (string fullName, string roleTitle) GetUser(long userId)
        {
            var user = _userApplication.GetUserBy(userId);
            return (user.FullName, user.RoleTitle);
        }

        public string GetFullAddress(long addressId)
        {
            var address = _addressApplication.GetAddress(addressId);
            return address.State + " - " + address.City + " - " + address.NeighborHood +
                   $"- {DataDictionaries.Number} " + address.Number + $" - {DataDictionaries.PostCode} " +
                   address.PostCode;
        }
    }
}

