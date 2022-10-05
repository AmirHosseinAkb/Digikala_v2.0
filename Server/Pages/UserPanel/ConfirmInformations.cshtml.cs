using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserManagement.Application.Contracts.User;
using UserManagement.Application.Contracts.User.UserPanel;

namespace Server.Pages.UserPanel
{
    [Authorize]
    public class ConfirmInformationsModel : PageModel
    {
        private readonly IUserApplication _userApplication;

        public ConfirmInformationsModel(IUserApplication userApplication)
        {
            _userApplication = userApplication;
        }

        private const string cookieName = "UserFullName";
        public UserInformationsViewModel UserInformationsVM { get; set; }
        public void OnGet()
        {
            UserInformationsVM = _userApplication.GetUserInformationsForShow();
        }

        [BindProperty]
        public FullNameCommand FullNameCommand { get; set; }
        [BindProperty]
        public EmailCommand EmailCommand { get; set; }
        [BindProperty]
        public PhoneNumberCommand PhoneNumberCommand { get; set; }
        [BindProperty]
        public NationalNumberCommand NationalNumberCommand { get; set; }
        [BindProperty]
        public RefundCommand RefundCommand { get; set; }
        [BindProperty]
        public BirthDateCommand BirthDateCommand { get; set; }
        [BindProperty]
        public PasswordCommand PasswordCommand { get; set; }


  

        public IActionResult OnPostConfirmUserFullName()
        {
            if (ModelState.GetFieldValidationState(nameof(FullNameCommand.FirstName)) == ModelValidationState.Invalid
                || ModelState.GetValidationState(nameof(FullNameCommand.LastName)) == ModelValidationState.Invalid)
                return RedirectToPage();
            var result=_userApplication.ConfirmUserFullName(FullNameCommand);
            return Content(result);
        }

        public IActionResult OnPostConfirmUserNationalNumber()
        {
            if (ModelState.GetFieldValidationState(nameof(NationalNumberCommand.NationalNumber)) == ModelValidationState.Invalid)
                return RedirectToPage();
            var result = _userApplication.ConfirmUserNationalNumber(NationalNumberCommand);
            return Content(result);
        }

        public IActionResult OnPostConfirmUserEmail()
        {
            if (ModelState.GetFieldValidationState(nameof(EmailCommand.Email)) == ModelValidationState.Invalid)
                return RedirectToPage();
            var result = _userApplication.ConfirmUserEmail(EmailCommand);
            if (!result.IsSucceeded)
            {
                return BadRequest(result.Message);
            }
            
            return Content(EmailCommand.Email.Replace(" ","").ToLower());
        }

        public IActionResult OnPostConfirmUserPhoneNumber()
        {
            if (ModelState.GetFieldValidationState(nameof(PhoneNumberCommand.PhoneNumber)) ==
                ModelValidationState.Invalid)
                return RedirectToPage();
            var result=_userApplication.ConfirmUserPhoneNumber(PhoneNumberCommand);
            if (!result.IsSucceeded)
            {
                return BadRequest(result.Message);
            }
            return Content(PhoneNumberCommand.PhoneNumber);
        }

        public IActionResult OnPostConfirmUserBirthDate()
        {
            if (ModelState.GetFieldValidationState(nameof(BirthDateCommand.BirthYear))==ModelValidationState.Invalid
                || ModelState.GetFieldValidationState(nameof(BirthDateCommand.BirthMonth))==ModelValidationState.Invalid
                || ModelState.GetFieldValidationState(nameof(BirthDateCommand.BirthDay))==ModelValidationState.Invalid)
                return RedirectToPage();
            var birthDate=_userApplication.ConfirmUserBirthDate(BirthDateCommand);
            return Content(birthDate);
        }

        public IActionResult OnPostConfirmUserRefundType()
        {
            if (ModelState.GetFieldValidationState(nameof(RefundCommand.AccountNumber)) == ModelValidationState.Invalid)
                return RedirectToPage();
            var result = _userApplication.ConfirmUserRefundType(RefundCommand);
            if (!result.IsSucceeded)
            {
                return BadRequest(result.Message);
            }

            return Content(result.Message);
        }

        public IActionResult OnPostConfirmUserPassword()
        {
            if (ModelState.GetFieldValidationState(nameof(PasswordCommand.CurrentPassword)) ==
                ModelValidationState.Invalid
                || ModelState.GetFieldValidationState(nameof(PasswordCommand.NewPassword)) ==
                ModelValidationState.Invalid
                || ModelState.GetFieldValidationState(nameof(PasswordCommand.RepeatNewPassword)) ==
                ModelValidationState.Invalid)
                return RedirectToPage();
            var result=_userApplication.ConfirmUserPassword(PasswordCommand);
            if (!result.IsSucceeded)
            {
                return BadRequest(result.Message);
            }
            return Content("-");
        }
    }
}
