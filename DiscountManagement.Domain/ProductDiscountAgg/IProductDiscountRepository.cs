namespace DiscountManagement.Domain.ProductDiscountAgg;

public interface IProductDiscountRepository
{
    IQueryable<ProductDiscount> GetProductDiscounts(long productId = 0, string startDate = "", string endDate = "");
}