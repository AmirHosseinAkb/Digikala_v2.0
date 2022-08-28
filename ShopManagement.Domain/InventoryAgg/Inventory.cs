using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Domain.InventoryAgg
{
    public class Inventory
    {
        public long InventoryId { get; private set; }
        public long ProductId { get; private set; }
        public int ProductCount { get;private set; }

        public Product Product { get;private set; }

        protected Inventory()
        {

        }
        public Inventory(long productId, int productCount)
        {
            ProductId = productId;
            ProductCount = productCount;
        }

        public void ChangeInventory(int count)
        {
            ProductCount += count;
        }
    }
}
