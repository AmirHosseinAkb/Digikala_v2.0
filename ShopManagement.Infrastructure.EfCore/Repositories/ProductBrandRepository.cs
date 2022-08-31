using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductBrandAgg;

namespace ShopManagement.Infrastructure.EfCore.Repositories
{
    public class ProductBrandRepository:IProductBrandRepository
    {
        private readonly ShopContext _context;

        public ProductBrandRepository(ShopContext context)
        {
            _context = context;
        }
        public IQueryable<ProductBrand> GetProductBrands(string title="")
        {
            IQueryable<ProductBrand> result = _context.ProductBrands;
            if (!string.IsNullOrEmpty(title))
            {
                result = result.Where(b => b.BrandTitle.Contains(title) || b.OtherLangTitle.Contains(title));
            }
            return result;
        }
    }
}
