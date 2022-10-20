using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;
using _01_Framework.Infrastructure;
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
        public Tuple<List<OrderQueryModel>,List<OrderQueryModel>,List<OrderQueryModel>> GetUserOrders()
        {
            var orders=_shopContext.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(i=>i.Product)
                .Where(o => o.UserId == _authenticationHelper.GetCurrentUserId())
                .Select(o => new OrderQueryModel()
                {
                    OrderId = o.OrderId,
                    TrackingNumber = o.TrackingNumber,
                    Status = o.Status,
                    CreationDate = o.CreationDate.ToShamsi(),
                    PaidPrice = o.PaidPrice,
                    PaymentType = o.PaymentType,
                    ImageNames = o.OrderItems.Select(p=>p.Product.ImageName).ToList()
                }).AsNoTracking();
            var notPidOrders = orders.Where(o => o.Status == OrderStatuses.NotPaid).ToList();
            var isWaitingOrders = orders.Where(o => o.Status == OrderStatuses.IsWaiting).ToList();
            var sentOrders = orders.Where(o => o.Status == OrderStatuses.OrderSent).ToList();
            return Tuple.Create(notPidOrders, isWaitingOrders, sentOrders);
        }
    }
}
