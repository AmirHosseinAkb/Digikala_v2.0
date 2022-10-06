using ShopManagement.Application.Contracts.Order;

namespace DigikalaQuery.Contracts.Services;

public interface ICartCalculatorService
{
    Cart ComputeCart(List<CartItem> cartItems);
}