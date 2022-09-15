using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigikalaQuery.Contracts.ProductColors;

namespace DigikalaQuery.Contracts.Product
{
    public class ProductQueryModel
    {
        public long ProductId { get; set; }
        public string Title { get; set; }
        public string OtherLangTitle { get; set; }
        public string GroupTitle { get; set; }
        public string? PrimaryGroupTitle { get; set; }
        public string? SecondaryGroupTitle { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public List<ProductColorQueryModel> ProductColors { get; set; }
    }
}
