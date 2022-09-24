using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Server.Extensions;
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
            AddressVm = _addressApplication.GetUserAddresses();
            return Page();
        }

        public IActionResult OnGetCreate()
        {
            return Partial("./Create");
        }

        public IActionResult OnPostCreate(CreateAddressCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var result = _addressApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnPostSetDefaultAddress(long addressId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

        }
    }
}
