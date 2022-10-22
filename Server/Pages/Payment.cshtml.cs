using _01_Framework.Application.ZarinPal;
using _01_Framework.Infrastructure;
using DigikalaQuery.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contracts.Order;
using UserManagement.Application.Contracts.Address;

namespace Server.Pages
{
    [Authorize]
    public class PaymentModel : PageModel
    {
        private readonly ICartCalculatorService _cartCalculatorService;
        private readonly IAddressApplication _addressApplication;
        private readonly IDiscountService _discountService;
        private readonly IZarinpalFactory _zarinpalFactory;
        private readonly IOrderApplication _orderApplication;
        public string ErrorMessage { get; set; }
        public static Cart Cart { get; set; } = new Cart();
        private const string cookieName = "cart_items";

        public PaymentModel(ICartCalculatorService cartCalculatorService, IAddressApplication addressApplication, IDiscountService discountService
        , IZarinpalFactory zarinpalFactory, IOrderApplication orderApplication)
        {
            _cartCalculatorService = cartCalculatorService;
            _addressApplication = addressApplication;
            _discountService = discountService;
            _zarinpalFactory = zarinpalFactory;
            _orderApplication = orderApplication;
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
            Cart.AddressId = addressId;
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

        [BindProperty]
        public CartPaymentCommand CartPaymentCommand { get; set; }
        public IActionResult OnGetConfirmPaymentTypeForOrder(CartPaymentCommand cartPaymentCommand)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var result = _orderApplication.CreateOrder(cartPaymentCommand, Cart);
            if (result.Item1.IsSucceeded)
            {
                if (cartPaymentCommand.PaymentType == PaymentTypes.PayFromWallet)
                {
                    var operationResult = _orderApplication.PayOrderFromWallet(result.Item2,Cart.CartItems);
                    if (operationResult.IsSucceeded)
                    {
                        Response.Cookies.Delete("cart_items");
                        return Redirect("/UserPanel/Wallet");
                    }
                    ErrorMessage = operationResult.Message;
                    return Page();
                }
                else
                {
                    var paymentResponse = _orderApplication.AddOrderPayment((int)Cart.RemainingPrice, result.Item2);
                    if (paymentResponse.Status == 100)
                        return Redirect("https://SandBox.Zarinpal.com/pg/StartPay/" + paymentResponse.Authority);
                    else
                        return NotFound();
                }

            }
            ErrorMessage = result.Item1.Message;
            return Page();
        }

        public IActionResult OnGetPayOrder(long transactionId, long orderId)
        {
            var requestQuery = HttpContext.Request.Query;
            if (requestQuery["Status"] != ""
                && requestQuery["Status"].ToString().ToLower() == "ok"
                && requestQuery["Authority"] != "")
            {
                var authority = requestQuery["Authority"];
                var verificationResponse =
                    _zarinpalFactory.CreateOrderVerificationRequest((int)Cart.RemainingPrice, authority);
                if (verificationResponse.Status == 100)
                {
                    _orderApplication.ConfirmOrderTransaction(transactionId);
                    _orderApplication.ConfirmOrder(orderId);
                    Response.Cookies.Delete("cart_items");
                    return Redirect("/CheckoutResult?isSuccessfull=true&refId=" + verificationResponse.RefId);
                }
            }
            return Redirect("/CheckoutResult?isSuccessfull=false");
        }

        public IActionResult OnGetRemoveDiscount()
        {
            Cart.OrderDiscountId = null;
            Cart.TotalOrderDiscount = 0;
            return RedirectToPage();
        }
    }
}
