using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductGroup;

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
        public void OnGet(long groupId)
        {
            command = new CreateGroupDetailCommand()
            {
                GroupId = groupId
            };
            GroupDetailVms = _productGroupApplication.GetDetailsOfGroup(groupId);
        }

        public IActionResult OnPostCreate()
        {
            ModelState.Remove("DetailId");
            if (!ModelState.IsValid)
                return BadRequest();
            var result = _productGroupApplication.CreateGroupDetail(command);
            return new JsonResult(result);
        }

        public IActionResult OnPostEdit()
        {
            if (!ModelState.IsValid || command.DetailId==null)
                return BadRequest();
            var result = _productGroupApplication.EditGroupDetail(command);
            return new JsonResult(result);
        }
    }
}
