using _01_Framework.Application;
using _01_Framework.Infrastructure;
using DigikalaQuery.Contracts.Order;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EfCore;
using UserManagement.Infrastructure.EfCore;

namespace DigikalaQuery.Queries
{
    public class OrderQuery:IOrderQuery
    {
        private readonly ShopContext _shopContext;
        private readonly AccountContext _accountContext;
        private readonly IAuthenticationHelper _authenticationHelper;

        public OrderQuery(ShopContext shopContext, IAuthenticationHelper authenticationHelper, AccountContext accountContext)
        {
            _shopContext = shopContext;
            _authenticationHelper = authenticationHelper;
            _accountContext = accountContext;
        }
        public Tuple<List<OrderQueryModel>,List<OrderQueryModel>,List<OrderQueryModel>,List<OrderQueryModel>> GetUserOrders()
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
                    OrderItems = o.OrderItems.Select(p=>p.Product)
                        .Select(p=>new OrderItemQueryModel() {ImageName = p.ImageName}).ToList()
                }).AsNoTracking();
            var notPidOrders = orders.Where(o => o.Status == OrderStatuses.NotPaid).ToList();
            var isWaitingOrders = orders.Where(o => o.Status == OrderStatuses.IsWaiting).ToList();
            var sentOrders = orders.Where(o => o.Status == OrderStatuses.OrderSent).ToList();
            var cancelledOrders = orders.Where(o => o.Status == OrderStatuses.Cancelled).ToList();
            return Tuple.Create(notPidOrders, isWaitingOrders, sentOrders,cancelledOrders);
        }

        public OrderQueryModel GetOrderDetails(long orderId)
        {
            var order= _shopContext.Orders
                .Include(o=>o.OrderItems)
                .ThenInclude(i=>i.ProductColor)
                .Include(o => o.OrderItems)
                .ThenInclude(i=>i.Product)
                .ThenInclude(p=>p.ProductColors)
                .SingleOrDefault(o => o.UserId==_authenticationHelper.GetCurrentUserId() && o.OrderId == orderId);

            if (order == null)
                return null;

            var orderAddress = _accountContext.Addresses.Single(a => a.AddressId == order.AddressId);
            var receiverName = orderAddress.ReceiverFirstName + " " + orderAddress.ReceiverLastName;
            return new OrderQueryModel()
            {
                TrackingNumber = order.TrackingNumber,
                PaymentType = order.PaymentType,
                CreationDate = order.CreationDate.ToShamsi(),
                OrderTotalPrice = order.OrderSum,
                OrderDiscountPrice = order.OrderDiscount,
                PaidPrice = order.PaidPrice,
                OrderId = order.OrderId,
                Status = order.Status,
                ReceiverName =receiverName,
                OrderItems = order.OrderItems.Select(i=>new OrderItemQueryModel()
                {
                    Title = i.Product.Title,
                    Count = i.Count,
                    UnitPrice = i.UnitPrice,
                    ColorCode = i.ProductColor.ColorCode,
                    ColorName = i.ProductColor.ColorName,
                    ImageName = i.Product.ImageName,
                    TotalPrice = i.UnitPrice*i.Count
                }).ToList()
            };
        }

        public List<OrderQueryModel> GetUserOrdersList()
        {
            return _shopContext.Orders.Where(o=>o.UserId==_authenticationHelper.GetCurrentUserId())
                .OrderByDescending(o=>o.CreationDate)
                .Take(6)
                .Select(o => new OrderQueryModel()
                {
                    OrderId = o.OrderId,
                    TrackingNumber = o.TrackingNumber,
                    Status = o.Status,
                    CreationDate = o.CreationDate.ToShamsi(),
                    OrderTotalPrice = o.OrderSum,
                    PaidPrice = o.PaidPrice,
                }).AsNoTracking().ToList();
        }
    }
}
