using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductGroupAgg;

namespace ShopManagement.Domain.ProductBrandAgg
{
    public class ProductBrand
    {
        public long BrandId { get;private set; }
        public long GroupId { get; set; }
        public string BrandTitle { get;private set; }
        public string OtherLangTitle { get;private set; }

        public List<Product> Products { get;private set; }
        public ProductGroup ProductGroup { get; set; }

        protected ProductBrand()
        {
            
        }

        public ProductBrand(string brandTitle, string otherLangTitle)
        {
            BrandTitle = brandTitle;
            OtherLangTitle = otherLangTitle;
        }
        public void Edit(string brandTitle, string otherLangTitle)
        {
            BrandTitle = brandTitle;
            OtherLangTitle = otherLangTitle;
        }
    }
}
