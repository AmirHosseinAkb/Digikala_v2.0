namespace ShopManagement.Domain.OrderAgg
{
    public class Order
    {
        public long OrderId { get;private set; }
        public long UserId { get;private set; }
        public long? DiscountId { get;private set; }
        public long? AddressId { get;private set; }
        public int OrderSum { get;private set; }
        public int OrderDiscount { get; private set; } = 0;
        public int PaidPrice { get; private set; }
        public string TrackingNumber { get;private set; }
        public byte PaymentType { get;private set; } // 1 For Payment From Wallet ;2 For Payment From Bank
        public byte Status { get;private set; }// 1 For Not Paid;2 For Is Waiting;3 For Order Sent
        public bool IsClosed { get;private set; }
        public DateTime CreationDate { get;private set; }
        public bool IsDeleted { get;private set; }

        public List<OrderItem> OrderItems { get; private set; }

        protected Order()
        {
            
        }

        public Order(long userId, long? discountId, long? addressId, int orderSum, string trackingNumber, byte paymentType, byte status, DateTime creationDate,int orderDiscount,int paidPrice)
        {
            UserId = userId;
            DiscountId = discountId;
            AddressId = addressId;
            OrderSum = orderSum;
            TrackingNumber = trackingNumber;
            PaymentType = paymentType;
            Status = status;
            CreationDate = creationDate;
            IsClosed = false;
            OrderDiscount=orderDiscount;
            PaidPrice = paidPrice;
        }

        public void Edit(long userId, long? discountId, long? addressId, int orderSum, string trackingNumber, byte paymentType, byte status, bool isClosed, DateTime creationDate,int orderDiscount,int paidPrice)
        {
            UserId = userId;
            DiscountId = discountId;
            AddressId = addressId;
            OrderSum = orderSum;
            TrackingNumber = trackingNumber;
            PaymentType = paymentType;
            Status = status;
            IsClosed = isClosed;
            CreationDate = creationDate;
            OrderDiscount=orderDiscount;
            PaidPrice = paidPrice;
        }

        public void Delete()
        {
            IsDeleted = true;
        }

        public void Confirm()
        {
            Status = 2;
            IsClosed = true;
        }

        public void ConfirmForSent()
        {
            Status = 3;
        }

        public void CancelOrder()
        {
            Status = 4;
        }
    }
}
