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
    }
}
