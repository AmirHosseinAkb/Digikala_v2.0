using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Server.Extensions;
using Server.Pages.Roles;
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
        public IActionResult OnGet(bool isAddressNotFound=false)
        {
            if (!_userApplication.IsUserInformationsConfirmed())
                return Redirect("/UserPanel/ConfirmInformations");
            AddressVm = _addressApplication.GetUserAddresses();
            ViewData["IsAddressNotFound"] = isAddressNotFound;
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

        public IActionResult OnGetSetDefaultAddress(long addressId)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var result = _addressApplication.SetDefaultAddress(addressId);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long addressId)
        {
            var address = _addressApplication.GetAddressForEdit(addressId);
            return Partial("./Edit",address);
        }

        public IActionResult OnPostEdit(EditAddressCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var result = _addressApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetDelete(long addressId)
        {
            TempData["AddressId"]=addressId;
            return Partial("./Delete");
        }

        public IActionResult OnPostDelete(long addressId)
        {
            if (!ModelState.IsValid)
                return RedirectToPage();
            var result=_addressApplication.Delete(addressId);
            
            return new JsonResult(result);
        }
    }
}
