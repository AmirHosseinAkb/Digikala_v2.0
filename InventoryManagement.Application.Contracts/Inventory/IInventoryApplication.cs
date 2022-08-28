using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Contracts.Inventory
{
    public interface IInventoryApplication
    {
        Tuple<List<InventoryViewModel>, int, int, int> GetInventories(int pageId = 1,long productId=0,
            bool isInStock = false,int take=10);
    }
}
