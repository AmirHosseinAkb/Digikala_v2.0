using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Pages.UserPanel
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            
        }
    }
}
