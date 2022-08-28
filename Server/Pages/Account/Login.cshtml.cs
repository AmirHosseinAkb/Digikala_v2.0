using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserManagement.Application.Contracts.User;

namespace Server.Pages.Account
{
    public class LoginModel : PageModel
    {

        private readonly IUserApplication _userApplication;
        public string ErrorMessage { get; set; }

        [BindProperty]
        public LoginCommand Command { get; set; }

        public LoginModel(IUserApplication userApplication)
        {
            _userApplication = userApplication;
        }
        public IActionResult OnGet()
        {
            var typedHeaders = HttpContext.Request.GetTypedHeaders();
            var httpReferer = typedHeaders.Referer?.AbsoluteUri;

            if (httpReferer == null || string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
                return RedirectToPage("RegisterAndLogin");

            return Page();
        }


        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();
            Command.Email = HttpContext.Session.GetString("Email");
            var result=_userApplication.Login(Command);
            if (!result.IsSucceeded)
            {
                ErrorMessage = result.Message;
                return Page();
            }
            return Redirect("/UserPanel");
        }
    }
}
