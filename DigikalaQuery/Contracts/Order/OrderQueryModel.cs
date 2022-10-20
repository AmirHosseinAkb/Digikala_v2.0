using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigikalaQuery.Contracts.Order
{
    public class OrderQueryModel
    {
        public string TrackingNumber { get; set; }
        public string CreationDate { get; set; }
        public int OrderTotalPrice { get; set; }
        public int OrderDiscountPrice { get; set; }
        public int RemainingPrice { get; set; }
        public byte Status { get; set; }
        public bool IsClosed { get; set; }
    }
}
