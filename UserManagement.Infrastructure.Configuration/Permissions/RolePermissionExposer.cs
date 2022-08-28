using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Infrastructure;

namespace UserManagement.Infrastructure.Configuration.Permissions
{
    internal class RolePermissionExposer:IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>()
            {
                {
                    "Roles",new List<PermissionDto>()
                    {
                        new PermissionDto("Roles List",RolePermissions.RolesList),
                        new PermissionDto("Create Role",RolePermissions.CreateRole),
                        new PermissionDto("Edit Role",RolePermissions.EditRole),
                        new PermissionDto("Delete Role",RolePermissions.DeleteRole),
                    }
                }
            };
        }
    }
}
