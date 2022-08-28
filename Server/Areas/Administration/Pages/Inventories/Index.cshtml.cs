using InventoryManagement.Application.Contracts.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
        public void OnGet(int pageId=1,long productId=0,bool isInStock=false,int take=10)
        {
            ViewData["Take"] = take;
            InventoryVms = _inventoryApplication.GetInventories(pageId, productId, isInStock, take);
        }
    }
}
