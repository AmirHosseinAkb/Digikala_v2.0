using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopManagement.Domain.ProductGroupAgg;

namespace ShopManagement.Domain.ProductAgg
{
    public interface IProductRepository
    {
        List<Product> GetAll(string title="",long groupId=0,long primaryGroupId=0,long secondaryGroupId=0);
        bool IsExistProduct(string title);
        long AddProduct(Product product);
        Product GetProductById(long productId);
        Product GetProductWithGroups(long productId);
        List<Product> GetProductsByGroupId(long groupId);
        void SaveChanges();
        Product GetProductByGroupId(long groupId);
        List<Product> GetProductsWithInventory();

        #region ProductDetail
        void AddProductDetails(GroupDetail detail);
        void AddProductDetails(Product product);
        void EditProductDetails(Product product);
        List<ProductDetail> GetProductDetails(long productId);
        void ConfirmProductDetails(long productId, Dictionary<int, string> details);
        Product? GetProductByBrandId(long brandId);

        #endregion
    }
}
