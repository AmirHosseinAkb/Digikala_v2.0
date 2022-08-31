using System.Net;
using _01_Framework.Application;

namespace ShopManagement.Application.Contracts.ProductBrand
{
    public interface IProductBrandApplication
    {
        Tuple<List<ProductBrandViewModel>,int,int,int> GetProductBrands(int pageId = 1, string title = "", int take = 20);
        OperationResult Create(CreateBrandCommand command);
        EditBrandCommand GetBrandForEdit(long brandId);
        OperationResult Edit(EditBrandCommand command);
    }
}
