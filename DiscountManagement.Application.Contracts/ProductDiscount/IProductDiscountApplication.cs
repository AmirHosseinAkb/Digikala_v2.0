using _01_Framework.Application;

namespace DiscountManagement.Application.Contracts.ProductDiscount;

public interface IProductDiscountApplication
{
    Tuple<List<ProductDiscountViewModel>, int, int, int> GetProductDiscounts(ProductDiscountSearchModel searchModel);
    OperationResult Create(CreateProductDiscountCommand command);
}