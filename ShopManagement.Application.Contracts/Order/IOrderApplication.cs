using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;
using Microsoft.AspNetCore.Mvc;

namespace ShopManagement.Application.Contracts.Order
{
    public interface IOrderApplication
    {
        OperationResult CreateOrder(CartPaymentCommand command, Cart cart);
    }
}
