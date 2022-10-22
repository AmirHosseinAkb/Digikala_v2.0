using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Domain.OrderAgg
{
    public interface IOrderRepository
    {
        Order GetUserOpenOrder(long userId);
        long AddOrder(Order order);
        Order GetOrderById(long orderId);
        void Update(Order order);
        void AddOrderItems(List<OrderItem> orderItems);
        void UpdateOrderItems(List<OrderItem> orderItems,long orderId);
        IQueryable<Order> GetOrders(byte? status=0, string trackingNumber="",  long? userId=0);
        List<OrderItem> GetOrderItems(long orderId);
        void SaveChanges();
    }
}
