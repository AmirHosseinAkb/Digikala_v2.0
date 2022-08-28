using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Domain.ProductImageAgg
{
    public interface IProductImageRepository
    {
        List<ProductImage> GetProductImages(long productId);
        void Add(ProductImage Image);
        ProductImage GetById(long imageId);
        void Delete(ProductImage image);
    }
}
