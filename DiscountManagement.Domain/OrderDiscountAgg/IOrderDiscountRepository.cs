namespace DiscountManagement.Domain.OrderDiscountAgg;

public interface IOrderDiscountRepository
{
    void Add(OrderDiscount orderDiscount);
    bool IsExistDiscount(string code);
    IQueryable<OrderDiscount> GetOrderDiscounts(string code = "",string reason="", bool isActive = false);
    OrderDiscount GetOrderDiscount(long discountId);
    void SaveChanges();
    void Delete(OrderDiscount orderDiscount);
}