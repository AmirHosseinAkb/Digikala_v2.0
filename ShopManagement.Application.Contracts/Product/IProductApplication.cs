using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.ProductGroup;

namespace ShopManagement.Application.Contracts.Product
{
    public interface IProductApplication
    {
        Tuple<List<ProductViewModel>,int,int,int> GetProducts(int pageId=1,string title="",long groupId=0,long primaryGroupId=0,long secondaryGroupId=0,int take=0);
        OperationResult Create(CreateProductCommand command);
        EditProductCommand GetProductForEdit(long productId);
        OperationResult Edit(EditProductCommand command);
        DeleteProductCommand GetProductForDelete(long productId);
        OperationResult Delete(long productId);
        bool IsExistProductByGroup(long groupId);
        bool CheckInputGroups(long groupId,long?primaryGroupId,long?secondaryGroupId);
        List<SelectListItem> GetProductsForSelect();

        #region ProductDetail
        List<ProductDetailViewModel> GetProductDetails(long productId);
        OperationResult ConfirmProductDetails(long productId, Dictionary<int, string> details);

        #endregion
    }
}
