using DigikalaQuery.Contracts.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Pages.UserPanel
{
    [Authorize]
    public class OrdersModel : PageModel
    {
        private readonly IOrderQuery _orderQuery;

        public OrdersModel(IOrderQuery orderQuery)
        {
            _orderQuery = orderQuery;
        }

        public Tuple<List<OrderQueryModel>,List<OrderQueryModel>,List<OrderQueryModel>,List<OrderQueryModel>> OrderQueryModels { get; set; }
        public void OnGet()
        {
            OrderQueryModels = _orderQuery.GetUserOrders();
        }
    }
}
