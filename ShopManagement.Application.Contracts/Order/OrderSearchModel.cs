using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application.Contracts.Order
{
    public class OrderSearchModel
    {
        public int PageId { get; set; } = 1;
        public string? TrackingNumber { get; set; } = "";
        public byte? Status { get; set; } = 0;
        public long? UserId { get; set; } = 0;
        public int Take { get; set; } = 10;
    }
}
