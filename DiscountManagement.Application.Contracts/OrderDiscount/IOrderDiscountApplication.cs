using System.Net;
using _01_Framework.Application;

namespace DiscountManagement.Application.Contracts.OrderDiscount
{
    public interface IOrderDiscountApplication
    {
        OperationResult Create(CreateOrderDiscountCommand command);
        Tuple<List<OrderDiscountViewModel>,int,int,int> GetOrderDiscounts(OrderDiscountSearchModel searchModel);
        EditOrderDiscountCommand GetDiscountForEdit(long discountId);
        OperationResult Edit(EditOrderDiscountCommand command);
        void ReduceDiscountUsableCount(long discountId);
        void Delete(long discountId);
    }
}
