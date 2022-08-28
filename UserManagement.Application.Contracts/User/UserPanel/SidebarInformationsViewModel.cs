using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Contracts.User.UserPanel
{
    public class SidebarInformationsViewModel
    {
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public long WalletBalance { get; set; } = 0;
        public string AvatarName { get; set; }
    }
}
