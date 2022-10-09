using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contracts.Order;

namespace Server.Pages
{
    public class PaymentModel : PageModel
    {
        public Cart Cart { get; set; }
        private const string cookieName = "cart_items";
        public void OnGet(long addressId)
        {
            var serializer = new JavaScriptSerializer();
            var cookie = Request.Cookies[cookieName];
            var cartItems = serializer.Deserialize<List<CartItem>>(cookie);
            foreach (var cartItem in cartItems)
            {
                cartItem.CalculateTotalItemPrice();
            }
        }
    }
}
