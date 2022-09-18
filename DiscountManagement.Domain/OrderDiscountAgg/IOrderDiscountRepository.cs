namespace DiscountManagement.Domain.OrderDiscountAgg;

public interface IOrderDiscountRepository
{
    void Add(OrderDiscount orderDiscount);
    bool IsExistDiscount(string code);
}