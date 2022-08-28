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
    }
}
