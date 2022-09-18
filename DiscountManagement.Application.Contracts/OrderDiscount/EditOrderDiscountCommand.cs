using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManagement.Application.Contracts.OrderDiscount
{
    public class EditOrderDiscountCommand:CreateOrderDiscountCommand
    {
        public long DiscountId { get; set; }
    }
}
