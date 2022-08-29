using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Infrastructure;

namespace ShopManagement.Infrastructure.Configuration.Permissions
{
    public class ProductPermissionExposer:IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>()
            {
                {
                    "Products",new List<PermissionDto>()
                    {
                        new PermissionDto("ProductList",ProductPermissions.ProductsList),
                        new PermissionDto("CreateProduct",ProductPermissions.CreateProduct),
                        new PermissionDto("EditProduct",ProductPermissions.EditProduct),
                        new PermissionDto("DeleteProduct",ProductPermissions.DeleteProduct),
                        new PermissionDto("CreateProductColor",ProductPermissions.CreateProductColor),
                        new PermissionDto("DeleteProductColor",ProductPermissions.DeleteProductColor),
                        new PermissionDto("ProductImagesList",ProductPermissions.ProductImagesList),
                        new PermissionDto("CreateProductImage",ProductPermissions.CreateProductImage),
                        new PermissionDto("DeleteProductImage",ProductPermissions.DeleteProductImage),
                        new PermissionDto("ConfirmProductDetails",ProductPermissions.ConfirmProductDetails),
                    }
                }
            };
        }
    }
}
