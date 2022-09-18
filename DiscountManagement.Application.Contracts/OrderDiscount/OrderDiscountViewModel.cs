using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManagement.Application.Contracts.OrderDiscount
{
    public class OrderDiscountViewModel
    {
        public OrderDiscountViewModel()
        {
            
        }
        public long DiscountId { get; set; }
        public string DiscountCode { get; set; }
        public string DiscountRate { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public int? UsableCount { get; set; }
        public string Reason { get; set; }

    }
}
