using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Pages
{
    public class CheckoutResultModel : PageModel
    {
        public IActionResult OnGet(bool isSuccessfull, string refId)
        {
            var typedHeaders = Request.GetTypedHeaders();
            var referer = typedHeaders.Referer?.AbsoluteUri;
            if (string.IsNullOrEmpty(referer))
                return Redirect("/");
            ViewData["IsSuccessfull"] = isSuccessfull;
            ViewData["RefId"] = refId;
            return Page();
        }
    }
}
