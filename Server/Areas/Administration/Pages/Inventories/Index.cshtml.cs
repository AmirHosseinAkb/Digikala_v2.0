using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Areas.Administration.Pages.Inventories
{
    public class IndexModel : PageModel
    {
        public IndexModel()
        {
            
        }
        public void OnGet(int pageId=1,long productId=0,bool isInStock=false,int take=10)
        {
            ViewData["Take"] = take;
        }
    }
}
