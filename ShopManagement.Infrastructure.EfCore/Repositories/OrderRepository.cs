using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.OrderAgg;
using UserManagement.Domain.UserAgg;
using UserManagement.Infrastructure.EfCore;

namespace ShopManagement.Infrastructure.EfCore.Repositories
{
    public class OrderRepository:IOrderRepository
    {
        private readonly ShopContext _shopContext;
        private readonly AccountContext _accountContext;

        public OrderRepository(ShopContext shopContext, AccountContext accountContext)
        {
            _shopContext = shopContext;
            _accountContext = accountContext;
        }
        public Order GetUserOpenOrder(long userId)
        {
            return _shopContext.Orders.FirstOrDefault(o => o.UserId == userId && !o.IsClosed);
        }

        public long AddOrder(Order order)
        {
            _shopContext.Orders.Add(order);
            _shopContext.SaveChanges();
            return order.OrderId;
        }

        public Order GetOrderById(long orderId)
        {
            return _shopContext.Orders.Find(orderId);
        }

        public void Update(Order order)
        {
            _shopContext.Orders.Update(order);
            _shopContext.SaveChanges();
        }

        public void AddOrderItems(List<OrderItem> orderItems)
        {
            foreach (var orderItem in orderItems)
            {
                _shopContext.OrderItems.Add(orderItem);
            }

            _shopContext.SaveChanges();
        }

        public void UpdateOrderItems(List<OrderItem> orderItems,long orderId)
        {
            _shopContext.OrderItems.Where(i=>i.OrderId==orderId).ToList().ForEach(i=>_shopContext.OrderItems.Remove(i));
            foreach (var orderItem in orderItems)
            {
                _shopContext.OrderItems.Add(orderItem);
            }

            _shopContext.SaveChanges();
        }

        public IQueryable<Order> GetOrders(byte? status=0, string trackingNumber="", long? userId=0)
        {
            IQueryable<Order> orders = _shopContext.Orders;

            if (!string.IsNullOrEmpty(trackingNumber))
                orders = orders.Where(o => o.TrackingNumber.Contains(trackingNumber));
            if (userId!=0 && userId!=null)
            {
                orders = orders.Where(o => o.UserId==userId);
            }
            if(status!=0 && status!=null)
                orders = orders.Where(o => o.Status==status);
            return orders;
        }

        public List<OrderItem> GetOrderItems(long orderId)
        {
            return _shopContext.OrderItems
                .Include(i=>i.Product)
                .Include(i=>i.ProductColor)
                .Where(i => i.OrderId == orderId).ToList();
        }

        public void SaveChanges()
        {
            _shopContext.SaveChanges();
        }
    }
}
