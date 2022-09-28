namespace DiscountManagement.Domain.ProductDiscountAgg;

public interface IProductDiscountRepository
{
    IQueryable<ProductDiscount> GetProductDiscounts(long productId = 0, string startDate = "", string endDate = "");
    bool IsExistProductDiscount(long productId,string startDate,string endDate);
    void Add(ProductDiscount productDiscount);
    void SaveChanges();
    ProductDiscount GetDiscountById(long discountId);
}