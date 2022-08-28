using _01_Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserManagement.Application.Contracts.Role;
using UserManagement.Application.Contracts.User;
using UserManagement.Application.Contracts.User.Administration;

namespace Server.Areas.Administration.Pages.Users
{
    public class IndexModel : PageModel
    {
        public IndexModel(IUserApplication userApplication, IRoleApplication roleApplication)
        {
            _userApplication = userApplication;
            _roleApplication = roleApplication;
        }
        private readonly IUserApplication _userApplication;
        private readonly  IRoleApplication _roleApplication;

        public string ErrorMessage { get; set; }
        public Tuple<List<UserAdminInformationsViewModel>,int,int,int> UsersInformationsVm { get; set; }
        [BindProperty]
        public CreateUserCommand CreateUserCommand { get; set; }

        [BindProperty] 
        public EditUserCommand EditUserCommand { get; set; }
        
        [NeedsPermission(UserPermissions.UsersList)]
        public void OnGet(int pageId = 1, string fullName = "", string email = "", string phoneNumber = "", int take = 20)
        {
            ViewData["Take"] = take;
            ViewData["Roles"] = _roleApplication.GetRoles();
            UsersInformationsVm = _userApplication.GetUsersAdminInformationsForShow(pageId,fullName,email,phoneNumber,take);
        }

        [NeedsPermission(UserPermissions.CreateUser)]
        public IActionResult OnPostCreateUser(long roleId)
        {
            ModelState.Remove("Email");
            ModelState.Remove("PhoneNumber");
            if (!ModelState.IsValid || roleId==0)
            {
                return RedirectToPage();
            }

            var result = _userApplication.AddUserFromAdmin(CreateUserCommand,roleId);
            if (!result.IsSucceeded)
            {
                ErrorMessage = result.Message;
            }

            return RedirectToPage();
        }

        [NeedsPermission(UserPermissions.EditUser)]
        public IActionResult OnPostEditUser(long roleId)
        {
            ModelState.Remove("Email");
            ModelState.Remove("PhoneNumber");

            if (!ModelState.IsValid)
                return RedirectToPage();
            var result = _userApplication.EditUserFromAdmin(EditUserCommand, roleId);
            if (!result.IsSucceeded)
            {
                ErrorMessage=result.Message;
            }
            return RedirectToPage();
        }

        [NeedsPermission(UserPermissions.DeleteUser)]
        public IActionResult OnPostDeleteUser(long userId)
        {
            _userApplication.DeleteUser(userId);
            return RedirectToPage();
        }

        public IActionResult OnGetIsExistEmailOrPhoneNumber(string email = "", string phoneNumber = "")
        {
            return Content(_userApplication.IsExistEmailOrPhoneNumber(email, phoneNumber).ToString().ToLower());
        }
    }
}
