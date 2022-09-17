using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManagement.Application.Contracts.OrderDiscount
{
    public class OrderDiscountSearchModel
    {
        public OrderDiscountSearchModel()
        {
            
        }
        public int PageId { get; set; }
        public string Code { get; set; }
        public string Reason { get; set; }
        public bool IsActive { get; set; }
        public int Take { get; set; }
    }
}
