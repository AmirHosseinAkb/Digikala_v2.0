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
        void SaveChanges();
    }
}
