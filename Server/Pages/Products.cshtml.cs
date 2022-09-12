using System.Collections;
using DigikalaQuery.Contracts.Product;
using DigikalaQuery.Contracts.ProductBrand;
using DigikalaQuery.Contracts.ProductColors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Product;

namespace Server.Pages
{
    public class ProductsModel : PageModel
    {
        private readonly IProductQuery _productQuery;

        public ProductsModel(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public SearchProductQueryModel SearchModel;

        public Tuple<List<ProductBoxQueryModel>, List<ProductColorQueryModel>, List<ProductBrandQueryModel>, int, int> ProductBoxVms { get; set; }
        public void OnGet(SearchProductQueryModel searchModel)
        {
            ProductBoxVms = _productQuery.GetProductsForShow(searchModel);
        }

        public IActionResult OnGetGetProducts(SearchProductQueryModel searchModel)
        {
            ViewData["PageId"] = searchModel.PageId;
            var prodcuts = _productQuery.GetProductsList(searchModel); 
            return Partial("PartialViews/_Products", prodcuts);
        }
    }
}
