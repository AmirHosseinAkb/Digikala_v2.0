using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Infrastructure;

namespace ShopManagement.Infrastructure.Configuration.Permissions
{
    public class InventoryPermissionExposer:IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>()
            {
                {
                    "Inventories", new List<PermissionDto>()
                    {
                        new PermissionDto("InventoriesList", InventoryPermissions.InventoriesList),
                        new PermissionDto("ChangeInventory", InventoryPermissions.ChangeInventory),
                        new PermissionDto("ShowInventoryHistory", InventoryPermissions.ShowInventoryHistory)
                    }
                }
            };
        }
    }
}
