using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;
using ShopManagement.Application.Contracts.Order;

namespace DigikalaQuery.Contracts.Services
{
    public interface IDiscountService
    {
        OperationResult ConfirmDiscount(string discountCode,Cart cart);
    }
}
