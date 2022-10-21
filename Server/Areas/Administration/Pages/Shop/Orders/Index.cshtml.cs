using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Order;
using UserManagement.Application.Contracts.User;

namespace Server.Areas.Administration.Pages.Shop.Orders
{
    public class IndexModel : PageModel
    {
        private readonly IOrderApplication _orderApplication;
        private readonly IUserApplication _userApplication;

        public IndexModel(IOrderApplication orderApplication,IUserApplication userApplication)
        {
            _orderApplication = orderApplication;
            _userApplication = userApplication;
        }

        public SelectList UsersSelectList { get; set; }
        public Tuple<List<OrderViewModel>,int,int,int> OrderVMs { get; set; }

        public OrderSearchModel SearchModel { get; set; }
        public void OnGet(OrderSearchModel searchModel)
        {
            ViewData["Take"] = searchModel.Take;
            UsersSelectList = new SelectList(_userApplication.GetUsersForSelect(), "Value", "Text");
            OrderVMs = _orderApplication.GetOrders(searchModel);
        }

        public IActionResult OnGetShowOrderDetails(long orderId)
        {

            return Partial("./OrderDetails");
        }
    }
}
