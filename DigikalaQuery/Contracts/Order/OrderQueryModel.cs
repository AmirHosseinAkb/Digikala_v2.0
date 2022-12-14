using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigikalaQuery.Contracts.Order
{
    public class OrderQueryModel
    {
        public long OrderId { get; set; }
        public string ReceiverName { get; set; }
        public string TrackingNumber { get; set; }
        public string CreationDate { get; set; }
        public int OrderTotalPrice { get; set; }
        public int OrderDiscountPrice { get; set; }
        public int PaidPrice { get; set; }
        public byte Status { get; set; }
        public byte PaymentType { get; set; }
        public List<OrderItemQueryModel> OrderItems { get; set; }

    }
}
