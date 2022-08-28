using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Domain.InventoryAgg
{
    public class InventoryHistory
    {
        public long InventoryHistoryId { get; private set; }
        public long ProductId { get; private set; }
        public long OperatorId { get;private set; }
        public int Count { get;private set; }
        public DateTime Date { get;private set; }

        public Product Product { get;private set; }
    }
}
