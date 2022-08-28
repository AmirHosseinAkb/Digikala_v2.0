using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Contracts.User.Administration
{
    public class UserAdminInformationsViewModel
    {
        public long UserId { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string? NationalNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public string RegisterDate { get; set; }
        public string AvatarName { get; set; }
        public long RoleId { get; set; }
        public string RoleTitle { get; set; }
    }
}
