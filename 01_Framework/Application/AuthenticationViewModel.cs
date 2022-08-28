using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Framework.Application
{
    public class AuthenticationViewModel
    {
        public long UserId { get; set; }
        public long RoleId { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string AvatarName { get; set; }
        public List<int> Permissions { get; set; }


        public AuthenticationViewModel(long userId,long roleId,string? email,string? phoneNumber,string avatarName,List<int> permissions)
        {
            UserId = userId;
            RoleId = roleId;
            Email = email;
            PhoneNumber = phoneNumber;
            Permissions = permissions;
            AvatarName = avatarName;
        }
    }
}
