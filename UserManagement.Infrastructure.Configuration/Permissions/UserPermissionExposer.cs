using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Infrastructure;

namespace UserManagement.Infrastructure.Configuration.Permissions
{
    public class UserPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>()
            {
                {"User",new List<PermissionDto>()
                    {
                        new PermissionDto("Users List",UserPermissions.UsersList),
                        new PermissionDto("Create User",UserPermissions.CreateUser),
                        new PermissionDto("Edit User",UserPermissions.EditUser),
                        new PermissionDto("Delete User",UserPermissions.DeleteUser),
                        new PermissionDto("Deleted UsersList",UserPermissions.DeletedUsersList),
                        new PermissionDto("Return User",UserPermissions.ReturnUser),
                        new PermissionDto("Search User",UserPermissions.SearchUser)
                    }
                }
            };
        }
    }
}
