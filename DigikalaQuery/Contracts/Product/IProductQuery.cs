using System.Reflection.Metadata;
using DigikalaQuery.Contracts.ProductBrand;
using DigikalaQuery.Contracts.ProductColors;

namespace DigikalaQuery.Contracts.Product
{
    public interface IProductQuery
    {
        Tuple<List<ProductBoxQueryModel>,List<ProductColorQueryModel>,List<ProductBrandQueryModel>, int, int> GetProductsForShow(int pageId = 1, string title = "",
            string orderBy = "", bool isInStock = false, int startPrice = 0, int endPrice = 0
            , List<int> brands = null, List<int> colors = null);
    }
}
