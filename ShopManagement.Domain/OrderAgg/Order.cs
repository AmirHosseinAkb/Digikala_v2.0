using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Domain.OrderAgg
{
    public class Order
    {
        public long OrderId { get;private set; }
        public long UserId { get;private set; }
        public long? DiscountId { get;private set; }
        public long? AddressId { get;private set; }
        public int OrderSum { get;private set; }
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

        public Order(long userId, long? discountId, long? addressId, int orderSum, string trackingNumber, byte paymentType, byte status, bool isClosed, DateTime creationDate, bool isDeleted)
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
            IsDeleted = isDeleted;
        }

        public void Edit(long userId, long? discountId, long? addressId, int orderSum, string trackingNumber, byte paymentType, byte status, bool isClosed, DateTime creationDate, bool isDeleted)
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
            IsDeleted = isDeleted;
        }

        public void Delete()
        {
            IsDeleted = true;
        }
    }
}
