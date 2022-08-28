using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Contracts.Inventory
{
    public class InventoryViewModel
    {
        public long InventoryId { get; set; }
        public long ProductId { get; set; }
        public string ImageName { get; set; }
        public string ProductTitle { get; set; }
        public int ProductPrice { get; set; }
        public int ProductCount { get; set; }
    }
}
