using DigikalaQuery.Contracts.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Pages.UserPanel
{
    [Authorize]
    public class OrderDetailsModel : PageModel
    {
        private readonly IOrderQuery _orderQuery;

        public OrderQueryModel OrderModel { get; set; }
        public OrderDetailsModel(IOrderQuery orderQuery)
        {
            _orderQuery = orderQuery;
        }
        public IActionResult OnGet(long orderId)
        {
            if (!ModelState.IsValid)
                return RedirectToPage();
            var order = _orderQuery.GetOrderDetails(orderId);
            if (order == null)
                return RedirectToPage();
            OrderModel = order;
            return Page();
        }
    }
}
