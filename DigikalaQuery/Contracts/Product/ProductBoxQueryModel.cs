using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopManagement.Domain.ProductColorAgg;

namespace DigikalaQuery.Contracts.Product
{
    public class ProductBoxQueryModel
    {
        public long ProductId { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public string ImageName { get; set; }
        public List<ProductColor> ProductColors { get; set; }
    }
}
