using DiscountManagement.Application.Contracts.OrderDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Areas.Administration.Pages.Discounts
{
    public class IndexModel : PageModel
    {
        private readonly IOrderDiscountApplication _discountApplication;

        public IndexModel(IOrderDiscountApplication discountApplication)
        {
            _discountApplication = discountApplication;
        }

        public OrderDiscountSearchModel SearchModel;
        public Tuple<List<OrderDiscountViewModel>,int,int,int> OrderDiscountVm { get; set; }
        public void OnGet(OrderDiscountSearchModel searchModel)
        {
            ViewData["Take"] = searchModel.Take;
            OrderDiscountVm = _discountApplication.GetOrderDiscounts(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            return Partial("./Create");
        }

        public IActionResult OnPostCreate(CreateOrderDiscountCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var result = _discountApplication.Create(command);
            return new JsonResult(result);
        }
    }
}
