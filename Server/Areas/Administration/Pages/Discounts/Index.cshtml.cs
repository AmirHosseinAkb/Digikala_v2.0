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
        public void OnGet(OrderDiscountSearchModel searchModel)
        {
            ViewData["Take"] = searchModel.Take;
        }
    }
}
