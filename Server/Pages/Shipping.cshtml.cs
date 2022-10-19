using DigikalaQuery.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contracts.Order;
using UserManagement.Application.Contracts.Address;

namespace Server.Pages
{
    public class ShippingModel : PageModel
    {
        private const string cookieName = "cart_items";
        private readonly IAddressApplication _addressApplication;
        public ICartCalculatorService _cartCalculatorService { get; set; }
        public ShippingModel(IAddressApplication addressApplication, ICartCalculatorService cartCalculatorService)
        {
            _addressApplication = addressApplication;
            AddressVm = new List<AddressViewModel>();
            _cartCalculatorService = cartCalculatorService;
        }
        public List<AddressViewModel> AddressVm { get; set; }
        public  Cart Cart { get; set; }
        public CartAddressCommand CartAddressCommand;
        public IActionResult OnGet()
        {
            var typedHeaders = Request.GetTypedHeaders();
            var referUrl = typedHeaders.Referer?.AbsoluteUri;
            if (string.IsNullOrEmpty(referUrl))
                return Redirect("/");
            AddressVm = _addressApplication.GetUserAddresses();
            if (!AddressVm.Any())
                return Redirect("/UserPanel/Addresses?isAddressNotFound=true");
            var serializer = new JavaScriptSerializer();
            var cookie = Request.Cookies[cookieName];
            var cartItems = serializer.Deserialize<List<CartItem>>(cookie);
            foreach (var item in cartItems)
            {
                item.CalculateTotalItemPrice();
            }

            Cart = _cartCalculatorService.ComputeCart(cartItems);
            return Page();
        }


        public IActionResult OnGetContinueToPayment(CartAddressCommand cartAddressCommand)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            if (_addressApplication.IsUserAddressExist(cartAddressCommand.AddressId))
            {
                var cookieOptions = new CookieOptions() {Expires = DateTime.Now.AddDays(14)};
                Response.Cookies.Append("User_Current_Address",
                    new JavaScriptSerializer().Serialize(cartAddressCommand.AddressId), cookieOptions);
                return RedirectToPage("Payment");
            }
            else
            {
                ViewData["AddressNotExist"] = true;
                return RedirectToPage();
            }
        }
    }
}
