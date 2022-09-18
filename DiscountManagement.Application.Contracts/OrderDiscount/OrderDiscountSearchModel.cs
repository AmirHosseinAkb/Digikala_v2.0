using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManagement.Application.Contracts.OrderDiscount
{
    public class OrderDiscountSearchModel
    {
        public int PageId { get; set; } = 1;
        public string? Code { get; set; } = "";
        public string? Reason { get; set; } = "";
        public bool IsActive { get; set; } = false;
        public int Take { get; set; } = 20;
    }
}
