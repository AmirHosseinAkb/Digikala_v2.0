using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;

namespace ShopManagement.Application.Contracts.Inventory
{
    public interface IInventoryApplication
    {
        Tuple<List<InventoryViewModel>,int,int,int> GetInventoriesForShow(int pageId, string title = "", bool isInStock = false,
            int take = 10);

        OperationResult ChangeInventory(ChangeInventoryCommand command);
        List<InventoryHistoryViewModel> GetInventoryHistories(long productId);
    }
}
