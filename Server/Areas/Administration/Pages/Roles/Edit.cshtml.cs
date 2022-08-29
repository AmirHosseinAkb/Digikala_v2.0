using _01_Framework.Application;
using _01_Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using UserManagement.Application.Contracts.Role;

namespace Server.Areas.Administration.Pages.Roles
{
    public class EditModel : PageModel
    {
        public EditModel(IRoleApplication roleApplication,IEnumerable<IPermissionExposer> exposers,IAuthenticationHelper authenticationHelper)
        {
            _roleApplication=roleApplication;
            _exposers=exposers;
            _authenticationHelper=authenticationHelper;
        }

        private readonly IEnumerable<IPermissionExposer> _exposers;
        private readonly IAuthenticationHelper _authenticationHelper;
        private readonly IRoleApplication _roleApplication;

        public string ErrorMessage { get; set; }
        [BindProperty]
        public EditRoleCommand Command { get; set; }

        public List<SelectListItem> Permissions { get; set; } = new List<SelectListItem>();

        public void GetInformations()
        {
            foreach (var exposer in _exposers)
            {
                var exposedPermissions = exposer.Expose();
                foreach (var (key,value) in exposedPermissions)
                {
                    var group = new SelectListGroup() {Name = key};
                    foreach (var permission in value)
                    {
                        var item = new SelectListItem(permission.PermissionTitle, permission.PermissionCode.ToString())
                        {
                            Group = group
                        };
                        if (Command.PermissionCodes.Any(c=>c==permission.PermissionCode))
                            item.Selected = true;
                        Permissions.Add(item);
                    }
                }
            }
        }

        [NeedsPermission(RolePermissions.EditRole)]
        public void OnGet(long roleId)
        {
            Command = _roleApplication.GetRoleForEdit(roleId);
            GetInformations();
        }
        
        [NeedsPermission(RolePermissions.EditRole)]
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return RedirectToPage();
            var result = _roleApplication.Edit(Command);
            if (!result.IsSucceeded)
            {
                ErrorMessage = result.Message;
                GetInformations();
                return Page();
            }
            return RedirectToPage("Index");
        }
    }
}
