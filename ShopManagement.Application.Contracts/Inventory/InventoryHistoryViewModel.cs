using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application.Contracts.Inventory
{
    public class InventoryHistoryViewModel
    {
        public long OperatorId { get; set; }
        public string OperatorName { get; set; }
        public string OperatorRole { get; set; }
        public int Count { get; set; }
        public DateTime Date { get; set; }
    }
}
