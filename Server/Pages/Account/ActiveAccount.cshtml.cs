using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserManagement.Application.Contracts.User;

namespace Server.Pages.Account
{
    public class ActiveAccountModel : PageModel
    {
        private readonly IUserApplication _userApplication;

        public ActiveAccountModel(IUserApplication userApplication)
        {
            _userApplication = userApplication;
        }
        public void OnGet(string activationCode)
        {
            ViewData["IsActivated"]=_userApplication.ActiveAccount(activationCode);;
        }
    }
}
