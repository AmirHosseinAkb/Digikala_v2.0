using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Domain.ProductBrandAgg
{
    public interface IProductBrandRepository
    {
        IQueryable<ProductBrand> GetProductBrands(string title="");
        bool IsExistBrand(string title,string otherlangTitle);
        void Add(ProductBrand productBrand);
        ProductBrand? GetBrand(long brandId);
        void SaveChanges();
        List<ProductBrand> GetProductBrands();
        void Delete(long brandId);
    }
}
