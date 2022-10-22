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
        private readonly IShopAccountAcl _shopAccountAcl;

        public OrderApplication(IOrderRepository orderRepository, IAuthenticationHelper authenticationHelper
            , IShopTransactionAcl shopTransactionAcl, IZarinpalFactory zarinpalFactory,IShopDiscountAcl shopDiscountAcl, IShopAccountAcl shopAccountAcl)
        {
            _orderRepository = orderRepository;
            _authenticationHelper = authenticationHelper;
            _shopTransactionAcl = shopTransactionAcl;
            _zarinpalFactory = zarinpalFactory;
            _shopDiscountAcl = shopDiscountAcl;
            _shopAccountAcl = shopAccountAcl;
        }
        public (OperationResult,long) CreateOrder(CartPaymentCommand command, Cart cart)
        {
            var result = new OperationResult();

            var orderStatuses = new List<int>() { OrderStatuses.NotPaid, OrderStatuses.IsWaiting, OrderStatuses.OrderSent };

            if (!orderStatuses.Contains(command.PaymentType))
                return (result.Failed(ApplicationMessages.PaymentTypeNotFound),0);

            var order = _orderRepository.GetUserOpenOrder(_authenticationHelper.GetCurrentUserId());
            var cPriceWithProDiscounts = cart.TotalCartPrice - cart.TotalProductDiscounts;
            var trackingNumber = CodeGenerator.GenerateTrackingNumber();

            if (order == null)
            {
                order = new Order(_authenticationHelper.GetCurrentUserId(), cart.OrderDiscountId, cart.AddressId,
                    (int)cPriceWithProDiscounts, trackingNumber, command.PaymentType, OrderStatuses.NotPaid, DateTime.Now
                    ,((cart.TotalOrderDiscount!=null)?(int)cart.TotalOrderDiscount:0),(int)cart.RemainingPrice);
                long orderId=_orderRepository.AddOrder(order);
                AddOrderItems(cart.CartItems,orderId);
                return (result.Succeeded(),orderId);
            }

            order.Edit(order.UserId,cart.OrderDiscountId,cart.AddressId,(int)(int)cPriceWithProDiscounts
                ,order.TrackingNumber,command.PaymentType,OrderStatuses.NotPaid,false,order.CreationDate
                , ((cart.TotalOrderDiscount!=null)?(int)cart.TotalOrderDiscount:0),(int)cart.RemainingPrice); //Because of maybe order informations change

            var orderItems =
                cart.CartItems.Select(i => new OrderItem(order.OrderId, i.Id, i.ColorId,(int)i.PayingPrice/i.Count, (byte) i.Count)).ToList();

            _orderRepository.UpdateOrderItems(orderItems,order.OrderId);

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
                cartItems.Select(i => new OrderItem(orderId, i.Id, i.ColorId,(int)i.PayingPrice/i.Count, (byte) i.Count)).ToList();
            _orderRepository.AddOrderItems(orderItems);
        }

        public OperationResult PayOrderFromWallet(long orderId,List<CartItem> cartItems)
        {
            //Fix This For Order
            var result = new OperationResult();
            var order = _orderRepository.GetOrderById(orderId);
            var userWalletBallance = _shopTransactionAcl.GetUserWalletBallance();
            if (order.OrderSum > userWalletBallance)
                return result.Failed(ApplicationMessages.WalletBallnceIsNotEnough);
            _shopTransactionAcl.AddtransactionForPayOrder(order.OrderSum,orderId);
            order.Confirm();
            _orderRepository.SaveChanges();
            AddOrderItems(cartItems,order.OrderId);
            return result.Succeeded();
        }

        public Tuple<List<OrderViewModel>, int, int, int> GetOrders(OrderSearchModel searchModel)
        {
            var orders = _orderRepository.GetOrders(searchModel.Status, searchModel.TrackingNumber, searchModel.UserId);
            var skip = (searchModel.PageId - 1) * searchModel.Take;
            var pageCount = orders.Count() / searchModel.Take;
            if (orders.Count() % searchModel.Take != 0)
                pageCount += 1;
            var query = orders.Skip(skip).Take(searchModel.Take).Select(o => new OrderViewModel()
            {
                OrderId = o.OrderId,
                UserId = o.UserId,
                OrderSum = o.OrderSum,
                TrackingNumber = o.TrackingNumber,
                Status = o.Status,
                CreationDate = o.CreationDate.ToShamsi(),
                OrderDiscount = o.OrderDiscount,
                PaidPrice = o.PaidPrice,
                UserName = _shopAccountAcl.GetUser(o.UserId).fullName,
                Address = _shopAccountAcl.GetFullAddress(o.AddressId!.Value)
            });
            return Tuple.Create(query.ToList(), searchModel.PageId, pageCount, searchModel.Take);
        }

        public List<OrderItemViewModel> GetOrderItems(long orderId)
        {
            return _orderRepository.GetOrderItems(orderId).Select(i => new OrderItemViewModel()
            {
                 ProductTitle = i.Product.Title,
                 ColorName = i.ProductColor.ColorName,
                 Count = i.Count,
                 UnitPrice = i.UnitPrice,
                 TotalPrice = i.UnitPrice*i.Count
            }).ToList();
        }

        public OperationResult ConfirmOrderForSent(long orderId)
        {
            var result = new OperationResult();
            var order = _orderRepository.GetOrderById(orderId);
            if (order == null)
                return result.Failed(ApplicationMessages.ProcessFailed);
            order.ConfirmForSent();
            _orderRepository.SaveChanges();

            return result.Succeeded();
        }

        public OperationResult CancelOrder(long orderId)
        {
            var result = new OperationResult();
            var order = _orderRepository.GetOrderById(orderId);
            if (order == null)
                return result.Failed(ApplicationMessages.ProcessFailed);
            order.CancelOrder();
            _orderRepository.SaveChanges();
            return result.Succeeded();
        }
    }
}
