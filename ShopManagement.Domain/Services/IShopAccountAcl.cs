using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Domain.Services
{
    public interface IShopAccountAcl
    {
        public (string fullName, string roleTitle) GetUser(long userId);
    }
}
