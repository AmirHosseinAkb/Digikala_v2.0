using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserManagement.Application.Contracts.Address;

namespace Server.Pages
{
    public class ShippingModel : PageModel
    {
        private readonly IAddressApplication _addressApplication;

        public ShippingModel(IAddressApplication addressApplication)
        {
            _addressApplication = addressApplication;
            AddressVm = new List<AddressViewModel>();
        }

        public List<AddressViewModel> AddressVm { get; set; }
        public IActionResult OnGet()
        {
            AddressVm = _addressApplication.GetUserAddresses();
            if (!AddressVm.Any())
                return Redirect("/UserPanel/Addresses?isAddressNotFound=true");
            return Page();
        }
    }
}
