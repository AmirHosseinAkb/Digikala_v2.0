using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserManagement.Application.Contracts.User;

namespace Server.Pages.Account
{
    public class ResetPasswordModel : PageModel
    {
        private readonly IUserApplication _userApplication;
        public string ErrorMessage { get; set; }

        public ResetPasswordModel(IUserApplication userApplication)
        {
            _userApplication = userApplication;
        }

        [BindProperty]
        public ResetPasswordCommand Command { get; set; }
        public void OnGet(string activationCode)
        {
            Command = new ResetPasswordCommand()
            {
                ActivationCode = activationCode
            };
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();
            var result = _userApplication.ResetPassword(Command);
            if (result.IsNull)
                return RedirectToPage("ResetPasswordFail");
            if (!result.IsSucceeded)
            {
                ErrorMessage=result.Message;
                return Page();
            }

            ViewData["SuccessReset"] = true;
            return Page();
        }
    }
}
