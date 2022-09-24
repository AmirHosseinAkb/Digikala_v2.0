using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductColorAgg;

namespace ShopManagement.Domain.OrderAgg
{
    public class OrderItem
    {
        public long OrderItemId { get;private set; }
        public long OrderId { get;private set; }
        public long ProductId { get;private set; }
        public long? ColorId { get;private set; }
        public int UnitPrice { get;private set; }
        public byte Count { get;private set; }


        public Order Order { get;private set; }
        public Product Product { get;private set; }
        public ProductColor ProductColor { get;private set; }

        protected OrderItem()
        {
            
        }

        public OrderItem(long orderId, long productId, long? colorId, int unitPrice, byte count)
        {
            OrderId = orderId;
            ProductId = productId;
            ColorId = colorId;
            UnitPrice = unitPrice;
            Count = count;
        }
    }
}
