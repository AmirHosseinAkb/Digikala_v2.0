using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserManagement.Application.Contracts.Address;
using UserManagement.Application.Contracts.User;

namespace Server.Pages.UserPanel
{
    public class AddressesModel : PageModel
    {
        private readonly IAddressApplication _addressApplication;
        private readonly IUserApplication _userApplication;
        public AddressesModel(IAddressApplication addressApplication, IUserApplication userApplication)
        {
            _addressApplication = addressApplication;
            _userApplication = userApplication;
        }

        public IActionResult OnGet()
        {
            if (!_userApplication.IsUserInformationsConfirmed())
                return Redirect("/UserPanel/ConfirmInformations");
            return Page();
        }
    }
}
