using _01_Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductGroup;
using ShopManagement.Infrastructure.Configuration.Permissions;

namespace Server.Areas.Administration.Pages.Shop.ProductGroups.GroupDetails
{
    public class IndexModel : PageModel
    {
        private readonly IProductGroupApplication _productGroupApplication;

        public IndexModel(IProductGroupApplication productGroupApplication)
        {
            _productGroupApplication = productGroupApplication;
        }

        public List<GroupDetailViewModel> GroupDetailVms { get; set; }
        [BindProperty]
        public CreateGroupDetailCommand command { get; set; }

        [NeedsPermission(ProductGroupPermissions.GroupDetailsList)]
        public void OnGet(long groupId)
        {
            command = new CreateGroupDetailCommand()
            {
                GroupId = groupId
            };
            GroupDetailVms = _productGroupApplication.GetDetailsOfGroup(groupId);
        }

        [NeedsPermission(ProductGroupPermissions.CreateGroupDetail)]
        public IActionResult OnPostCreate()
        {
            ModelState.Remove("DetailId");
            if (!ModelState.IsValid)
                return BadRequest();
            var result = _productGroupApplication.CreateGroupDetail(command);
            return new JsonResult(result);
        }

        [NeedsPermission(ProductGroupPermissions.EditGroupDetail)]
        public IActionResult OnPostEdit()
        {
            if (!ModelState.IsValid || command.DetailId==null)
                return BadRequest();
            var result = _productGroupApplication.EditGroupDetail(command);
            return new JsonResult(result);
        }
    }
}
