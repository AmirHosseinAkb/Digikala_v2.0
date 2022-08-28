using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductGroup;

namespace Server.Areas.Administration.Pages.Shop.Products.ProductDetails
{
    public class IndexModel : PageModel
    {
        private readonly IProductApplication _productApplication;

        public IndexModel(IProductApplication productApplication)
        {
            _productApplication = productApplication;
        }

        public List<ProductDetailViewModel> ProductDetailVms { get; set; }
        public void OnGet(long productId)
        {
            @ViewData["ProductId"] = productId;
            ProductDetailVms = _productApplication.GetProductDetails(productId);
        }

        public IActionResult OnPost(long productId,Dictionary<int,string> details)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var result = _productApplication.ConfirmProductDetails(productId, details);
            return new JsonResult(result);
        }
    }
}
