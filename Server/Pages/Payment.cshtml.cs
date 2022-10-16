using DigikalaQuery.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using Server.Extensions;
using ShopManagement.Application.Contracts.Order;
using UserManagement.Application.Contracts.Address;

namespace Server.Pages
{
    public class PaymentModel : PageModel
    {
        private readonly ICartCalculatorService _cartCalculatorService;
        private readonly IAddressApplication _addressApplication;
        private readonly IDiscountService _discountService;
        public static Cart Cart { get; set; } = new Cart();
        private const string cookieName = "cart_items";

        public PaymentModel(ICartCalculatorService cartCalculatorService, IAddressApplication addressApplication, IDiscountService discountService)
        {
            _cartCalculatorService = cartCalculatorService;
            _addressApplication = addressApplication;
            _discountService = discountService;
        }
        public IActionResult OnGet()
        {
            var typedHeaders = Request.GetTypedHeaders();
            var referUrl = typedHeaders.Referer?.AbsoluteUri;
            if (string.IsNullOrEmpty(referUrl))
                return Redirect("/");
            var serializer = new JavaScriptSerializer();
            var addressCookie = Request.Cookies["User_Current_Address"];
            var addressId = serializer.Deserialize<long>(addressCookie);
            if (!_addressApplication.IsUserAddressExist(addressId))
                return RedirectToPage("Shipping");
            var cookie = Request.Cookies[cookieName];
            var cartItems = serializer.Deserialize<List<CartItem>>(cookie);
            foreach (var cartItem in cartItems)
            {
                cartItem.CalculateTotalItemPrice();
            }

            if (Cart.OrderDiscountId == null)
                Cart = _cartCalculatorService.ComputeCart(cartItems);
            return Page();
        }

        [BindProperty]
        public CartDiscountCommand CartDiscountCommand { get; set; }
        public IActionResult OnPostConfirmDiscount(CartDiscountCommand cartDiscountCommand)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var result = _discountService.ConfirmDiscount(cartDiscountCommand.DiscountCode.Replace(" ", ""), Cart);
            if (result.IsSucceeded)
                ViewData["DiscountConfirmed"] = true;
            return new JsonResult(result);
        }
    }
}
