using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Domain.Services
{
    public interface IShopDiscountAcl
    {
        void ReduceDiscountUsableCount(long discountId);
    }
}
