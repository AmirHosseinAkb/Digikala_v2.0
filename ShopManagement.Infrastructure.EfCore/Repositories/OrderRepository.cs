using ShopManagement.Domain.OrderAgg;

namespace ShopManagement.Infrastructure.EfCore.Repositories
{
    public class OrderRepository:IOrderRepository
    {
        private readonly ShopContext _shopContext;

        public OrderRepository(ShopContext shopContext)
        {
            _shopContext = shopContext;
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

        public void SaveChanges()
        {
            _shopContext.SaveChanges();
        }
    }
}
