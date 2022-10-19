using _01_Framework.Application;
using _01_Framework.Application.ZarinPal;
using _01_Framework.Infrastructure;
using _01_Framework.Resources;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.Services;

namespace ShopManagement.Application
{
    public class OrderApplication : IOrderApplication
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IAuthenticationHelper _authenticationHelper;
        private readonly IShopTransactionAcl _shopTransactionAcl;
        private readonly IZarinpalFactory _zarinpalFactory;
        private readonly IShopDiscountAcl _shopDiscountAcl;

        public OrderApplication(IOrderRepository orderRepository, IAuthenticationHelper authenticationHelper
            , IShopTransactionAcl shopTransactionAcl, IZarinpalFactory zarinpalFactory,IShopDiscountAcl shopDiscountAcl)
        {
            _orderRepository = orderRepository;
            _authenticationHelper = authenticationHelper;
            _shopTransactionAcl = shopTransactionAcl;
            _zarinpalFactory = zarinpalFactory;
            _shopDiscountAcl = shopDiscountAcl;
        }
        public (OperationResult,long) CreateOrder(CartPaymentCommand command, Cart cart)
        {
            var result = new OperationResult();
            var orderStatuses = new List<int>() { OrderStatuses.NotPaid, OrderStatuses.IsWaiting, OrderStatuses.OrderSent };
            if (!orderStatuses.Contains(command.PaymentType))
                return (result.Failed(ApplicationMessages.PaymentTypeNotFound),0);
            var order = _orderRepository.GetUserOpenOrder(_authenticationHelper.GetCurrentUserId());
            var trackingNumber = CodeGenerator.GenerateTrackingNumber();
            if (order == null)
            {
                order = new Order(_authenticationHelper.GetCurrentUserId(), cart.OrderDiscountId, cart.AddressId,
                    (int)cart.TotalCartPrice, trackingNumber, command.PaymentType, OrderStatuses.NotPaid, DateTime.Now);
                long orderId=_orderRepository.AddOrder(order);
                return (result.Succeeded(),orderId);
            }
            order.Edit(order.UserId,cart.OrderDiscountId,cart.AddressId,(int)cart.RemainingPrice
                ,order.TrackingNumber,command.PaymentType,OrderStatuses.NotPaid,false,order.CreationDate); //Because of maybe order informations change
            _orderRepository.SaveChanges();
            return (result.Succeeded(), order.OrderId);
        }

        public long AddOrderTransaction(int amount,long orderId)
        {
            return _shopTransactionAcl.AddTransaction(amount,orderId);
        }

        public PaymentResponse AddOrderPayment(int amount,long orderId)
        {
            var transactionId = AddOrderTransaction(amount,orderId);
            return _zarinpalFactory.CreateOrderPaymentRequest(transactionId,orderId, amount,
                DataDictionaries.WithdrawTransactionDescription+orderId);
        }

        public void ConfirmOrderTransaction(long transactionId)
        {
            _shopTransactionAcl.confirmTransaction(transactionId);
        }

        public void ConfirmOrder(long orderId)
        {
            var order = _orderRepository.GetOrderById(orderId);
            order.Confirm();
            if (order.DiscountId != null)
                _shopDiscountAcl.ReduceDiscountUsableCount(order.DiscountId.Value);
            _orderRepository.Update(order);
        }

        public void AddOrderItems(List<CartItem> cartItems,long orderId)
        {
            var orderItems =
                cartItems.Select(i => new OrderItem(orderId, i.Id, i.ColorId, i.UnitPrice, (byte) i.Count)).ToList();
            _orderRepository.AddOrderItems(orderItems);
        }

        public OperationResult PayOrderFromWallet(long orderId)
        {
            var result = new OperationResult();
            var order = _orderRepository.GetOrderById(orderId);
            var userWalletBallance = _shopTransactionAcl.GetUserWalletBallance();
            if (order.OrderSum > userWalletBallance)
                return result.Failed(ApplicationMessages.WalletBallnceIsNotEnough);
            _shopTransactionAcl.AddTransaction(order.OrderSum,orderId);
            return result.Succeeded();
        }
    }
}
