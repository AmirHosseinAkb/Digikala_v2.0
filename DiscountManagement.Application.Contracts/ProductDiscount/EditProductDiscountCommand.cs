using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManagement.Application.Contracts.ProductDiscount
{
    public class EditProductDiscountCommand:CreateProductDiscountCommand
    {
        public long DiscountId { get; set; }
    }
}
