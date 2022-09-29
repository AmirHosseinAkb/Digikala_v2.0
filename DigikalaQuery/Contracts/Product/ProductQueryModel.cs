using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigikalaQuery.Contracts.ProductColors;
using DigikalaQuery.Contracts.ProductDetail;
using DigikalaQuery.Contracts.ProductImage;

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
        public string ImageName { get; set; }
        public int InventoryCount { get; set; }
        public string BrandName { get; set; }
        public int? DiscountRate { get; set; }
        public List<ProductColorQueryModel> ProductColors { get; set; }
        public List<ProductImageQueryModel> ProductImages { get; set; }
        public List<ProductDetailQueryModel> ProductDetails { get; set; }
    }
}
