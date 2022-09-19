using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Domain.OrderAgg
{
    public class Order
    {
        public long OrderId { get; set; }
        public long? DiscountId { get; set; }
        public long? AddressId { get; set; }
        public long UserId { get; set; }
        public int OrderSum { get; set; }
        public byte PaymentType { get; set; } // 1 For Payment From Wallet ;2 For Payment From Bank
        public byte Status { get; set; }// 1 For Not Paid;2 For Is Waiting;3 For Order Sent
        public bool IsClosed { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
