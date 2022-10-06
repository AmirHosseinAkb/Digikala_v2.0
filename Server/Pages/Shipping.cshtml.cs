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

        public ShippingModel(IAddressApplication addressApplication)
        {
            _addressApplication = addressApplication;
            AddressVm = new List<AddressViewModel>();
        }
        public List<AddressViewModel> AddressVm { get; set; }
        public List<CartItem> CartItemVm { get; set; }
        public IActionResult OnGet()
        {
            AddressVm = _addressApplication.GetUserAddresses();
            if (!AddressVm.Any())
                return Redirect("/UserPanel/Addresses?isAddressNotFound=true");
            var serializer = new JavaScriptSerializer();
            var cookie = Request.Cookies[cookieName];
            CartItemVm = serializer.Deserialize<List<CartItem>>(cookie);
            foreach (var item in CartItemVm)
            {
                item.CalculateTotalItemPrice();
            }
            return Page();
        }
    }
}
