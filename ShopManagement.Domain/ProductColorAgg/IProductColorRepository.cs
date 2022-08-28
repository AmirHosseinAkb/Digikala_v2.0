using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Domain.ProductColorAgg
{
    public interface IProductColorRepository
    {
        bool IsExistColor(long productId, string colorName, string colorCode);
        void Add(ProductColor color);
        List<ProductColor> GetAll();
        ProductColor GetById(long colorId);
        void Delete(ProductColor color);
    }
}
