using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Domain.RoleAgg
{
    public interface IRoleRepository
    {
        List<Role> GetRoles();
        Role GetRoleById(long roleId);
        void Add(Role role);
        bool IsExistRoleByTitle(string title);
        void DeleteRolePermissions(Role role);
        void SaveChanges();
    }
}
