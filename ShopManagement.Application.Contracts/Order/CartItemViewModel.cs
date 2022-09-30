using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application.Contracts.Order
{
    public class CartItemViewModel
    {
        public long Id { get; set; }
        public string Title{ get; set; }
        public int UnitPrice { get; set; }
        public int? DiscountRate { get; set; }
        public string ImageName{ get; set; }
        public int Count { get; set; }
        public string Brand { get; set; }
        public string? ColorName { get; set; }
        public string? ColorCode { get; set; }
        public int TotalItemPrice { get; set; }

    }
}
