using DiscountManagement.Application.Contracts.ProductDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;

namespace Server.Areas.Administration.Pages.Discounts.ProductDiscounts
{
    public class IndexModel : PageModel
    {
        private readonly IProductDiscountApplication _productDiscountApplication;
        private readonly IProductApplication _productApplication;

        public IndexModel(IProductDiscountApplication productDiscountApplication, IProductApplication productApplication)
        {
            _productDiscountApplication = productDiscountApplication;
            _productApplication = productApplication;
        }

        public ProductDiscountSearchModel SearchModel { get; set; }
        public List<SelectListItem> ProductsSelect { get; set; }
        public Tuple<List<ProductDiscountViewModel>,int,int,int> ProductDiscountVm  { get; set; }
        public void OnGet(ProductDiscountSearchModel searchModel)
        {
            ProductsSelect = _productApplication.GetProductsForSelect();
            ProductDiscountVm = _productDiscountApplication.GetProductDiscounts(searchModel);
            ViewData["Take"] = 10;
        }

        public IActionResult OnGetCreate()
        {
            return Partial("./Create");
        }

        public IActionResult OnPostCreate(CreateProductDiscountCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var result = _productDiscountApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long discountId)
        {
            var discount = _productDiscountApplication.GetDiscountForEdit(discountId);
            return Partial("./Edit" , discount);
        }

        public IActionResult OnPostEdit(EditProductDiscountCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var result = _productDiscountApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetDelete(long discountId)
        {
            TempData["DiscountId"]=discountId;
            return Partial("./Delete");
        }

        public IActionResult OnPostDelete(long discountId)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            _productDiscountApplication.Delete(discountId);
            return RedirectToPage();
        }
    }
}
