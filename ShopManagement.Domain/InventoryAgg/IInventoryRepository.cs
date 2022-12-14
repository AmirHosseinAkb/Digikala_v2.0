using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Domain.InventoryAgg
{
    public interface IInventoryRepository
    {
        List<Inventory> GetAll(string title="",bool isInStock=false);
        Inventory GetInventry(long id);
        void AddInventoryHistory(InventoryHistory history);
        List<InventoryHistory> GetInventoryHistories(long productId);
        void AddInventory(Inventory inventory);
    }
}
