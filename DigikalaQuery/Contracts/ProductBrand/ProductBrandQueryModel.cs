using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigikalaQuery.Contracts.ProductBrand
{
    public class ProductBrandQueryModel
    {
        public long BrandId { get; set; }
        public string BrandTitle { get; set; }
        public string OtherLangTitle { get; set; }
    }
}
