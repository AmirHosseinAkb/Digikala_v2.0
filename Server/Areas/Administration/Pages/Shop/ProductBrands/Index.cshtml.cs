using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductBrand;

namespace Server.Areas.Administration.Pages.Shop.ProductBrands
{
    public class IndexModel : PageModel
    {
        private readonly IProductBrandApplication _productBrandApplication;

        public IndexModel(IProductBrandApplication productBrandApplication)
        {
            _productBrandApplication = productBrandApplication;
        }

        public Tuple<List<ProductBrandViewModel>,int,int,int> ProductBrandVms { get; set; }
        public void OnGet(int pageId=1,string title="",int take=20)
        {
            if (take % 20 != 0)
                take = 20;
            ViewData["Take"] = take;
            ProductBrandVms = _productBrandApplication.GetProductBrands(pageId,title,take);
        }

        public IActionResult OnGetCreate()
        {
            return Partial("./Create");
        }

        public IActionResult OnPostCreate(CreateBrandCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = _productBrandApplication.Create(command);
            return new JsonResult(result);
        }
    }
}
