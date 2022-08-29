using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Infrastructure;

namespace ShopManagement.Infrastructure.Configuration.Permissions
{
    public class ProductGroupPermissionExposer:IPermissionExposer

    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>()
            {
                {
                    "ProductGroups", new List<PermissionDto>()
                    {
                        new PermissionDto("PoductGroupsList",ProductGroupPermissions.PoductGroupsList),
                        new PermissionDto("CreateGroup",ProductGroupPermissions.CreateGroup),
                        new PermissionDto("EditGroup",ProductGroupPermissions.EditGroup),
                        new PermissionDto("DeleteGroup",ProductGroupPermissions.DeleteGroup),
                        new PermissionDto("GroupDetailsList",ProductGroupPermissions.GroupDetailsList),
                        new PermissionDto("CreateGroupDetail",ProductGroupPermissions.CreateGroupDetail),
                        new PermissionDto("EditGroupDetail",ProductGroupPermissions.EditGroupDetail)
                    }
                }
            };
        }
    }
}
