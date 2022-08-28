using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Domain.ProductColorAgg
{
    public class ProductColor
    {
        public long ColorId { get; private set; }
        public long ProductId { get; private set; }
        public string ColorName { get; private set; }
        public string ColorCode { get; private set; }
        public Product Product { get;private set; }


        protected ProductColor()
        {
            
        }
        public ProductColor(long productId,string colorName,string colorCode)
        {
            ProductId=productId;
            ColorName=colorName;
            ColorCode=colorCode;
        }
    }
}
