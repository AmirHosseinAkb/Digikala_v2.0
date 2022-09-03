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
            _productQuery=productQuery;
        }

        public Tuple<List<ProductBoxQueryModel>,List<ProductColorQueryModel>,List<ProductBrandQueryModel>,int,int> ProductBoxVms { get; set; }
        public void OnGet(int pageId=1,string title="",string orderBy="",bool isInStock=false,int startPrice=0,int endPrice=0
            ,List<int> brands=null,List<int> colors=null)
        {
            ProductBoxVms = _productQuery.GetProductsForShow(pageId, title, orderBy, isInStock, startPrice, endPrice,
                brands, colors);
        }
    }
}
