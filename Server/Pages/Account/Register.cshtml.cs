using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserManagement.Application.Contracts.User;

namespace Server.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly IUserApplication _userApplication;
        public string ErrorMessage { get; set; }
        public RegisterModel(IUserApplication userApplication)
        {
            _userApplication = userApplication;
        }

        [BindProperty]
        public RegisterCommand Command { get; set; }

        public IActionResult  OnGet()
        {
            var typedHeaders = HttpContext.Request.GetTypedHeaders();
            var httpReferer = typedHeaders.Referer?.AbsoluteUri;
            if (httpReferer==null ||string.IsNullOrEmpty(HttpContext.Session.GetString("Email")))
                 return RedirectToPage("RegisterAndLogin");

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            Command.Email = HttpContext.Session.GetString("Email");
            var result = _userApplication.Register(Command);
            if (result.IsSucceeded)
            {
                return RedirectToPage("RegisterSuccess");
            }
            ErrorMessage = result.Message;
            return Page();
        }
    }
}
