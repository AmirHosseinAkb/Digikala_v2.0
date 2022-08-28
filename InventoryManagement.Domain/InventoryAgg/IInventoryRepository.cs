using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Domain.InventoryAgg
{
    public interface IInventoryRepository
    {
        List<Inventory> GetInventories(long productId=0, bool isInStock = false);
        List<Tuple<long,int,string,string>> GetProducts();
    }
}
