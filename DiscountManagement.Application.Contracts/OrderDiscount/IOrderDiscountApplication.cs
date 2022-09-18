using _01_Framework.Application;

namespace DiscountManagement.Application.Contracts.OrderDiscount
{
    public interface IOrderDiscountApplication
    {
        OperationResult Create(CreateOrderDiscountCommand command);
    }
}
