using _01_Framework.Application;
using _01_Framework.Application.ZarinPal;

namespace ShopManagement.Application.Contracts.Order
{
    public interface IOrderApplication
    {
        (OperationResult,long) CreateOrder(CartPaymentCommand command, Cart cart);
        long AddOrderTransaction(int amount,long orderId);
        PaymentResponse AddOrderPayment(int amount,long orderId);
        void ConfirmOrderTransaction(long transactionId);
        void ConfirmOrder(long orderId);
        void AddOrderItems(List<CartItem> cartItems,long orderId);
        OperationResult PayOrderFromWallet(long orderId,List<CartItem> cartItems);

        Tuple<List<OrderViewModel>, int, int, int> GetOrders(OrderSearchModel searchModel);
        List<OrderItemViewModel> GetOrderItems(long orderId);
    }
}
