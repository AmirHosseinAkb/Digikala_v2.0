using DigikalaQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Pages
{
    public class ProductModel : PageModel
    {
        private IProductQuery _productQuery;

        public ProductModel(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }
        public ProductQueryModel? Product { get; set; }
        public IActionResult OnGet(long productId)
        {
            Product = _productQuery.GetProduct(productId);
            if (Product == null)
                return RedirectToPage("/Products");
            return Page();
        }
    }
}
