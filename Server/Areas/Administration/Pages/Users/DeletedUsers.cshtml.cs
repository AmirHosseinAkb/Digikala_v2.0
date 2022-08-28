using _01_Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserManagement.Application.Contracts.User;
using UserManagement.Application.Contracts.User.Administration;

namespace Server.Areas.Administration.Pages.Users
{
    public class DeletedUsersModel : PageModel
    {
        private readonly IUserApplication _userApplication;

        public DeletedUsersModel(IUserApplication userApplication)
        {
            _userApplication = userApplication;
        }

        public Tuple<List<UserAdminInformationsViewModel>,int,int,int> UsersInformationsVm { get; set; }
        [NeedsPermission(UserPermissions.DeletedUsersList)]
        public void OnGet(int pageId = 1, string fullName = "", string email = "",
            string phoneNumber = "", int take = 20)
        {
            ViewData["Take"] = take;
            UsersInformationsVm =
                _userApplication.GetDeletedUsersAdminInformationsForShow(pageId, fullName, email, phoneNumber, take);
        }

        public IActionResult OnPostReturnUser(long userId)
        {
            _userApplication.ReturnUser(userId);
            return RedirectToPage();
        }
    }
}
