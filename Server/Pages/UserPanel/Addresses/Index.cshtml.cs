using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserManagement.Application.Contracts.Address;
using UserManagement.Application.Contracts.User;

namespace Server.Pages.UserPanel.Addresses
{
    public class IndexModel : PageModel
    {
        private readonly IAddressApplication _addressApplication;
        private readonly IUserApplication _userApplication;
        public IndexModel(IAddressApplication addressApplication, IUserApplication userApplication)
        {
            _addressApplication = addressApplication;
            _userApplication = userApplication;
        }

        public List<AddressViewModel> AddressVm { get; set; }
        public IActionResult OnGet()
        {
            if (!_userApplication.IsUserInformationsConfirmed())
                return Redirect("/UserPanel/ConfirmInformations");
            AddressVm = _addressApplication.GetAddresses();
            return Page();
        }
    }
}
