using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Domain.Services
{
    public interface IShopAccountAcl
    {
        (string fullName, string roleTitle) GetUser(long userId);
        string GetFullAddress(long addressId);
    }
}
