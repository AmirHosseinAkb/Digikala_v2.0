using DigikalaQuery.Contracts.Order;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Server.Pages.UserPanel
{
    public class OrdersModel : PageModel
    {
        private readonly IOrderQuery _orderQuery;

        public OrdersModel(IOrderQuery orderQuery)
        {
            _orderQuery = orderQuery;
        }

        public List<OrderQueryModel> OrderQueryModels { get; set; }
        public void OnGet()
        {
            OrderQueryModels = _orderQuery.GetUserOrders();
        }
    }
}
