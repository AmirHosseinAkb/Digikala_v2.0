using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Domain.ProductImageAgg
{
    public class ProductImage
    {
        public long ImageId { get; private set; }
        public long ProductId { get;private set; }
        public string ImageName { get; private set; }

        public Product Product { get;private set; }


        protected ProductImage()
        {
            
        }
        public ProductImage(long productId,string imageName)
        {
            ImageName = imageName;
            ProductId = productId;
        }
    }
}
