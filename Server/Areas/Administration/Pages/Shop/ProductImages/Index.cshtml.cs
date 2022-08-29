using _01_Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductImage;
using ShopManagement.Infrastructure.Configuration.Permissions;

namespace Server.Areas.Administration.Pages.Shop.ProductImages
{
    public class IndexModel : PageModel
    {
        private readonly IProductImageApplication _productImageApplication;

        public IndexModel(IProductImageApplication productImageApplication)
        {
            _productImageApplication = productImageApplication;
        }
        public List<ProductImageViewModel> ProductImages { get; set; }

        [NeedsPermission(ProductPermissions.ProductImagesList)]
        public void OnGet(long productId)
        {
            ProductImages = _productImageApplication.GetProductImages(productId);
        }
        [NeedsPermission(ProductPermissions.CreateProductImage)]
        public IActionResult OnGetCreate(long productId)
        {
            return Partial("./Create",new CreateImageCommand(){ProductId = productId});
        }

        [NeedsPermission(ProductPermissions.CreateProductImage)]
        public IActionResult OnPostCreate(CreateImageCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var result = _productImageApplication.Create(command);
            return new JsonResult(result);
        }

        [NeedsPermission(ProductPermissions.DeleteProductImage)]
        public IActionResult OnPostDelete(long imageId)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var result = _productImageApplication.Delete(imageId);
            return Redirect("/Administration/Shop/Products");
        }
    }
}
