using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Contracts.Role
{
    public class DeleteRoleCommand
    {
        public long RoleId { get; set; }
    }
}
