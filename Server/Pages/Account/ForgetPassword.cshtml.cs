using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using UserManagement.Application.Contracts.User;

namespace Server.Pages.Account
{
    public class ForgetPasswordModel : PageModel
    {
        private readonly IUserApplication _userApplication;
        public string ErrorMessage { get; set; }
        
        public string? Email { get; set; }

        public ForgetPasswordModel(IUserApplication userApplication)
        {
            _userApplication = userApplication;
        }

        [BindProperty] 
        public ForgetPasswordCommand Command { get; set; }
        public IActionResult OnGet()
        {
            var typedHeaders = HttpContext.Request.GetTypedHeaders();
            var httpreferer = typedHeaders.Referer.AbsoluteUri;
            if (string.IsNullOrEmpty(httpreferer))
                return RedirectToPage("RegisterAndLogin");

            Email = TempData["Email"]?.ToString();
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();
            var result = _userApplication.ForgetPassword(Command);
            if (!result.IsSucceeded)
            {
                ErrorMessage = result.Message;
                return Page();
            }

            ViewData["IsEmailSent"] = true;
            return Page();
        }
    }
}
