namespace DiscountManagement.Application.Contracts.ProductDiscount;

public interface IProductDiscountApplication
{
    Tuple<List<ProductDiscountViewModel>, int, int, int> GetProductDiscounts(ProductDiscountSearchModel searchModel);
}