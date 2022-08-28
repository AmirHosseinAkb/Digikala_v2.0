using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Inventory;

namespace Server.Areas.Administration.Pages.Inventories
{
    public class IndexModel : PageModel
    {
        private readonly IInventoryApplication _inventoryApplication;

        public IndexModel(IInventoryApplication inventoryApplication)
        {
            _inventoryApplication = inventoryApplication;
        }

        public Tuple<List<InventoryViewModel>,int,int,int> InventoryVms { get; set; }
        public void OnGet(int pageId=1,string title="",bool isInStock=false,int take=10)
        {
            if (take % 10 != 0)
                take = 10;
            ViewData["Take"] = take;
            ViewData["IsInStock"] = isInStock;
            InventoryVms = _inventoryApplication.GetInventoriesForShow(pageId,title,isInStock,take);
        }


        public IActionResult OnGetChangeInventory(long inventoryId, bool isForDecrease = false)
        {

            TempData["IsForDecrease"] = isForDecrease;
            return Partial("./ChangeInventory",new ChangeInventoryCommand(){InventoryId = inventoryId,IsForDecrease = isForDecrease});
        }

        public IActionResult OnPostChangeInventory(ChangeInventoryCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var result = _inventoryApplication.ChangeInventory(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetShowInventoryHistories(long productId)
        {
            var histories = _inventoryApplication.GetInventoryHistories(productId);
            return Partial("./InventoryHistory", histories);
        }
    }
}
