using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application.Contracts.Order
{
    public class OrderItemViewModel
    {
        public string ProductTitle { get; set; }
        public string ColorName { get; set; }
        public int UnitPrice { get; set; }
        public int Count { get; set; }
        public int TotalPrice { get; set; }
    }
}
