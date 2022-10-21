using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application.Contracts.Order
{
    public class OrderViewModel
    {
        public long OrderId { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public string CreationDate { get; set; }
        public byte Status { get; set; }
        public int OrderSum { get; set; }
        public int OrderDiscount { get; set; }
        public int PaidPrice { get; set; }
        public string TrackingNumber { get; set; }
    }
}
