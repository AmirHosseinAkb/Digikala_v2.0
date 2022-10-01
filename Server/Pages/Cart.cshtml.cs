using DigikalaQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contracts.Order;

namespace Server.Pages
{
    public class CartModel : PageModel
    {
        public const string cookieName = "cart_items";
        public List<CartItemViewModel> CartItemVm { get; set; }
        private readonly IProductQuery _productQuery;

        public CartModel(IProductQuery productQuery)
        {
            _productQuery=productQuery;
        }
        public void OnGet()
        {
            var serializer = new JavaScriptSerializer();
            var cookie = Request.Cookies["cart_items"];
            var cartItems = serializer.Deserialize<List<CartItemViewModel>>(cookie);

            foreach (var item in cartItems)
            {
                item.TotalItemPrice = item.UnitPrice * item.Count;
            }

            CartItemVm = _productQuery.CheckInventoryStatus(cartItems);
        }

        public IActionResult OnGetRemoveFromCart(long id)
        {
            var cookie = Request.Cookies[cookieName];
            var serializer=new JavaScriptSerializer();
            var items=serializer.Deserialize<List<CartItemViewModel>>(cookie);
            Response.Cookies.Delete(cookieName);
            var toRemoveItem = items.FirstOrDefault(i => i.Id == id);
            if(toRemoveItem!=null)
                items.Remove(toRemoveItem);
            var cookieOptions = new CookieOptions() {Expires = DateTime.Now.AddDays(10), Path = "/"};
            Response.Cookies.Append(cookieName,serializer.Serialize(items),cookieOptions);
            return RedirectToPage();
        }
    }
}
