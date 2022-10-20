using DigikalaQuery.Contracts.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Pages.UserPanel
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IOrderQuery _orderQuery;

        public IndexModel(IOrderQuery orderQuery)
        {
            _orderQuery = orderQuery;
        }

        public List<OrderQueryModel> OrderModels { get; set; }
        public void OnGet()
        {
            OrderModels = _orderQuery.GetUserOrdersList();
        }
    }
}
