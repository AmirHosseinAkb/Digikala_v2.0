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
        public long ItemId { get; set; }
        public long OrderId { get; set; }
        public long ProductId { get; set; }
        public long ColorId { get; set; }
        public int UnitPrice { get; set; }
        public byte Count { get; set; }


        public Order Order { get; set; }
        public Product Product { get; set; }
        public ProductColor ProductColor { get; set; }
    }
}
