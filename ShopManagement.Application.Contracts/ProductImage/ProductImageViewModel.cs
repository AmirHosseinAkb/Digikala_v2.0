using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application.Contracts.ProductImage
{
    public class ProductImageViewModel
    {
        public long ImageId { get; set; }
        public long ProductId { get; set; }
        public string ImageName { get; set; }
    }
}
