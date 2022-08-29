using _01_Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductColor;
using ShopManagement.Infrastructure.Configuration.Permissions;

namespace Server.Areas.Administration.Pages.Shop.ProductColors
{
    public class IndexModel : PageModel
    {
        private readonly IProductColorApplication _productColorApplication;

        public IndexModel(IProductColorApplication productColorApplication)
        {
            _productColorApplication = productColorApplication;
        }

        [NeedsPermission(ProductPermissions.CreateProductColor)]
        public IActionResult OnGetCreate(long productId)
        {
            return Partial("./Create",new CreateColorCommand(){ProductId = productId});
        }

        [NeedsPermission(ProductPermissions.CreateProductColor)]
        public IActionResult OnPostCreate(CreateColorCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var result = _productColorApplication.Create(command);
            return new JsonResult(result);
        }

        [NeedsPermission(ProductPermissions.DeleteProductColor)]
        public IActionResult OnPostDelete(long colorId)
        {
            if (!ModelState.IsValid)
                return Redirect("/Administration/Shop/Products");
            _productColorApplication.Delete(colorId);
            return Redirect("/Administration/Shop/Products");
        }
    }
}
