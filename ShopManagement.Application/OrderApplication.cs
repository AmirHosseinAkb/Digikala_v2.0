using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;
using _01_Framework.Infrastructure;
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

        public OrderApplication(IOrderRepository orderRepository, IAuthenticationHelper authenticationHelper, IShopTransactionAcl shopTransactionAcl)
        {
            _orderRepository = orderRepository;
            _authenticationHelper = authenticationHelper;
            _shopTransactionAcl = shopTransactionAcl;
        }
        public OperationResult CreateOrder(CartPaymentCommand command, Cart cart)
        {
            var result = new OperationResult();
            var order = _orderRepository.GetUserOpenOrder(_authenticationHelper.GetCurrentUserId());

            var orderStatuses = new List<int>() {1, 2, 3};
            if (!orderStatuses.Contains(command.PaymentType))
                return result.Failed(ApplicationMessages.PaymentTypeNotFound);

            var trackingNumber = CodeGenerator.GenerateTrackingNumber();
            order = new Order(_authenticationHelper.GetCurrentUserId(), cart.OrderDiscountId, cart.AddressId,
                (int)cart.TotalCartPrice
                , trackingNumber, command.PaymentType, OrderStatuses.NotPaid, false, DateTime.Now);

            var orderId = _orderRepository.AddOrder(order);
            if (command.PaymentType == PaymentTypes.PayFromWallet)
            {
                
            }

            return result.Succeeded();
        }
    }
}
