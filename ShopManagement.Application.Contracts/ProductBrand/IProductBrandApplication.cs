using System.Net;

namespace ShopManagement.Application.Contracts.ProductBrand
{
    public interface IProductBrandApplication
    {
        Tuple<List<ProductBrandViewModel>,int,int,int> GetProductBrands(int pageId = 1, string title = "", int take = 20);
    }
}
