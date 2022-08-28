using _01_Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using UserManagement.Application.Contracts.Role;

namespace Server.Areas.Administration.Pages.Roles
{
    public class CreateModel : PageModel
    {
        public CreateModel(IRoleApplication roleApplication,IEnumerable<IPermissionExposer> exposers)
        {
            _roleApplication=roleApplication;
            _exposers=exposers;
        }
        private readonly IRoleApplication _roleApplication;

        private readonly IEnumerable<IPermissionExposer> _exposers;

        public string ErrorMessage { get; set; }

        [BindProperty]
        public CreateRoleCommand Command { get; set; }

        public List<SelectListItem> Permissions { get; set; } = new List<SelectListItem>();

        public void GetInformations()
        {
            foreach (var exposer in _exposers)
            {
                var exposedPermissions = exposer.Expose();
                foreach (var (key,value) in exposedPermissions)
                {
                    foreach (var permission in value)
                    {
                        Permissions.Add( new SelectListItem(permission.PermissionTitle, permission.PermissionCode.ToString())
                        {
                            Group = new SelectListGroup(){Name = key}
                        });
                    }
                }
            }
        }

        [NeedsPermission(RolePermissions.CreateRole)]
        public void OnGet()
        {
            GetInformations();
        }

        [NeedsPermission(RolePermissions.CreateRole)]
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return RedirectToPage();
            
            var result=_roleApplication.Create(Command);
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
