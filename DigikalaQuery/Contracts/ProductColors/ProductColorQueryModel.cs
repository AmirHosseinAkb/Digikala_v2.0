using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigikalaQuery.Contracts.ProductColors
{
    public class ProductColorQueryModel
    {
        public long ColorId { get; set; }
        public string ColorName { get; set; }
        public string ColorCode { get; set; }
    }
}
