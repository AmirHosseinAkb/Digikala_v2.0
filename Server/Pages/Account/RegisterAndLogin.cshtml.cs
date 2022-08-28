using _01_Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserManagement.Application.Contracts.User;

namespace Server.Pages.Account
{
    public class RegisterAndLoginModel : PageModel
    {
        public string ErrorMessage { get; set; }
        public RegisterAndLoginModel(IUserApplication userApplication)
        {
            _userApplication = userApplication;
        }


        private readonly IUserApplication _userApplication;
        
        [BindProperty]
        public RegisterAndLoginCommand Command { get; set; }
        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();
            var result = _userApplication.RegisterAndLogin(Command);
            if (!result.IsSucceeded)
            {
                ErrorMessage = result.Message;
                return Page();
            }
            if (Command.EmailOrPhone.IsEmail())
            {
                HttpContext.Session.SetString("Email",Command.EmailOrPhone);
                if (_userApplication.IsExistByEmail(Command.EmailOrPhone))
                {
                    TempData["Email"] = Command.EmailOrPhone;
                    return RedirectToPage("Login");
                }
                else
                    return RedirectToPage("Register");
            }
            else if (Command.EmailOrPhone.IsPhoneNumber())
            {
                HttpContext.Session.SetString("PhoneNumber",Command.EmailOrPhone);
                return RedirectToPage("Verification");
            }

            return Page();
        }

        public IActionResult OnGetLogout()
        {
            _userApplication.SignOut();
            return Redirect("/");
        }
    }
}
