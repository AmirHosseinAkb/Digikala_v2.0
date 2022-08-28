using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;

namespace UserManagement.Application.Contracts.Role
{
    public class EditRoleCommand
    {
        public long RoleId { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(200,ErrorMessage = ValidationMessages.MaxLength)]
        public string RoleTitle { get; set; }

        public List<int>? PermissionCodes { get; set; }
    }
}
