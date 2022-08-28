using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application.Contracts.Product
{
    public class ProductDetailViewModel
    {
        public long DetailId { get; set; }
        public string DetailTitle { get; set; }
        public string? DetailValue { get; set; }
    }
}
