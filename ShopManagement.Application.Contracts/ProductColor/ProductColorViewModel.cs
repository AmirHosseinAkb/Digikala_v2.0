using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application.Contracts.ProductColor
{
    public class ProductColorViewModel
    {
        public long ColorId { get; set; }
        public long ProductId { get; set; }
        public string ColorName { get; set; }
        public string ColorCode { get; set; }
    }
}
