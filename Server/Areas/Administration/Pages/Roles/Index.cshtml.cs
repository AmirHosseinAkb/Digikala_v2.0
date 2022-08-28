using _01_Framework.Infrastructure;
using _01_Framework.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using UserManagement.Application.Contracts.Role;
using UserManagement.Application.Contracts.User;

namespace Server.Areas.Administration.Pages.Roles
{
    public class IndexModel : PageModel
    {
        public string ErrorMessage { get; set; }
        public IndexModel(IRoleApplication roleApplication,IUserApplication userApplication)
        {
            _roleApplication = roleApplication;
            _userApplication = userApplication;
        }
        private readonly IRoleApplication _roleApplication;
        private readonly IUserApplication _userApplication;


        public List<RoleViewModel> RoleVm { get; set; }

        [NeedsPermission(RolePermissions.RolesList)]
        public void OnGet()
        {
            RoleVm = _roleApplication.GetRoles();
        }


        public IActionResult OnGetDelete(long roleId)
        {
            if (_userApplication.IsExistUserByRoleId(roleId))
                return BadRequest(ErrorMessages.DeleteRoleErrorMessage);
            return Partial("./Delete");
        }
        [NeedsPermission(RolePermissions.DeleteRole)]
        public IActionResult OnPostDelete(long roleId)
        {
            var result = _roleApplication.Delete(roleId);
            if (!result.IsSucceeded)
            {
                RoleVm = _roleApplication.GetRoles();
                ErrorMessage = result.Message;
                return Page();
            }
            return RedirectToPage();
        }
    }
}
