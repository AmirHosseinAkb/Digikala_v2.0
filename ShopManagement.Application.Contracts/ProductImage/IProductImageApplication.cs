using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;

namespace ShopManagement.Application.Contracts.ProductImage
{
    public interface IProductImageApplication
    {
        List<ProductImageViewModel> GetProductImages(long productId);
        OperationResult Create(CreateImageCommand command);
        OperationResult Delete(long imageId);
    }
}
