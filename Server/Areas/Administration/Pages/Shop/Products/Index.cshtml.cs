using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductColor;
using ShopManagement.Application.Contracts.ProductGroup;

namespace Server.Areas.Administration.Pages.Shop.Products
{
    public class IndexModel : PageModel
    {
        private readonly IProductApplication _productApplication;
        private readonly IProductGroupApplication _productGroupApplication;
        private readonly IProductColorApplication _productColorApplication;

        public Tuple<List<ProductViewModel>, int, int, int> ProductVM { get; set; }
        public List<ProductColorViewModel> ProductColorVms { get; set; }
        public IndexModel(IProductApplication productApplication, IProductGroupApplication productGroupApplication,IProductColorApplication productColorApplication)
        {
            _productApplication = productApplication;
            _productGroupApplication = productGroupApplication;
            _productColorApplication = productColorApplication;
        }

        public IActionResult OnGet(int pageId = 1, string title = "", long groupId = 0, long primaryGroupId = 0, long secondaryGroupId = 0, int take = 10)
        {
            if (!_productGroupApplication.IsExistAnyGroup())
            {
                TempData["IsExistGroup"] = true;
                return Redirect("/Administration/Shop/ProductGroups");

            }
            if (take % 10 != 0)
                take = 10;
            ViewData["Take"] = take;

            var groups = _productGroupApplication.GetGroups(null);
            ViewData["Groups"] = new SelectList(groups, "Value", "Text");

            var primaryGroups = _productGroupApplication.GetGroups(int.Parse(groups.First().Value));
            ViewData["PrimaryGroups"] = new SelectList(primaryGroups, "Value", "Text");

            var secondaryGroups = new List<SelectListItem>();
            if(primaryGroups.Count()!=0)
                secondaryGroups = _productGroupApplication.GetGroups(int.Parse(primaryGroups.First().Value));
            ViewData["SecondaryGroups"] = new SelectList(secondaryGroups, "Value", "Text");

            ProductVM = _productApplication.GetProducts(pageId, title, groupId, primaryGroupId, secondaryGroupId, take);
            ProductColorVms = _productColorApplication.GetAll();
            return Page();
        }

        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreateProductCommand());
        }

        public IActionResult OnPostCreate(CreateProductCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = _productApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long productId)
        {
            var product = _productApplication.GetProductForEdit(productId);
            return Partial("./Edit", product);
        }

        public IActionResult OnPostEdit(EditProductCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var result = _productApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetDelete(long productId)
        {
            var command = _productApplication.GetProductForDelete(productId);
            return Partial("./Delete", command);
        }

        public IActionResult OnPostDelete(long productId)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var result = _productApplication.Delete(productId);
            return new JsonResult(result);
        }

        public IActionResult OnGetGetSubGroups(long id)
        {
            var subGroups = _productGroupApplication.GetGroups(id);
            return new JsonResult(new SelectList(subGroups, "Value", "Text"));
        }
    }
}
