using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contracts.Order;

namespace Server.Pages
{
    public class CartModel : PageModel
    {
        public List<CartItemViewModel> CartItemVm { get; set; }
        public void OnGet()
        {
            var serializer = new JavaScriptSerializer();
            var cookie = Request.Cookies["cart_items"];
            CartItemVm = serializer.Deserialize<List<CartItemViewModel>>(cookie);

            foreach (var item in CartItemVm)
            {
                item.TotalItemPrice = item.UnitPrice * item.Count;
            }
        }
    }
}
