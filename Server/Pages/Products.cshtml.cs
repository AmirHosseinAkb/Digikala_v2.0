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

        [BindProperty]
        public SearchProductQueryModel SearchModel { get; set; }

        public Tuple<List<ProductBoxQueryModel>, List<ProductColorQueryModel>, List<ProductBrandQueryModel>, int, int> ProductBoxVms { get; set; }
        public void OnGet()
        {
            SearchModel=new SearchProductQueryModel();
            ProductBoxVms = _productQuery.GetProductsForShow(SearchModel);
        }

        public IActionResult OnGetGetProducts(SearchProductQueryModel searchModel)
        {
            var prodcuts = _productQuery.GetProductsList(searchModel);
            return Partial("PartialViews/_Products", prodcuts);
        }
    }
}
