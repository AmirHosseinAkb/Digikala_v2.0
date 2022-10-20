using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;
using DigikalaQuery.Contracts.Order;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EfCore;

namespace DigikalaQuery.Queries
{
    public class OrderQuery:IOrderQuery
    {
        private readonly ShopContext _shopContext;
        private readonly IAuthenticationHelper _authenticationHelper;

        public OrderQuery(ShopContext shopContext, IAuthenticationHelper authenticationHelper)
        {
            _shopContext = shopContext;
            _authenticationHelper = authenticationHelper;
        }
        public List<OrderQueryModel> GetUserOrders()
        {
            return null;
            //return _shopContext.Orders.Include(o => o.OrderItems)
            //    .Where(o => o.UserId == _authenticationHelper.GetCurrentUserId())
            //    .Select(o => new OrderQueryModel()
            //    {
            //        TrackingNumber = o.TrackingNumber,
            //        Status = o.Status,
            //        CreationDate = o.CreationDate.ToShamsi(),
            //        OrderTotalPrice = o.OrderSum+

            //    }).ToList();
        }
    }
}
