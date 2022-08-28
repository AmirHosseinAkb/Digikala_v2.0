using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;

namespace UserManagement.Application.Contracts.Role
{
    public interface IRoleApplication
    {
        List<RoleViewModel> GetRoles();
        OperationResult Create(CreateRoleCommand command);
        EditRoleCommand GetRoleForEdit(long roleId);
        OperationResult Edit(EditRoleCommand Command);
        OperationResult Delete(long roleId);
    }
}
