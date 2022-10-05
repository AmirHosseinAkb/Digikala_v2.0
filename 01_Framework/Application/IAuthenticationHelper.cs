using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Operators;

namespace _01_Framework.Application
{
    public interface IAuthenticationHelper
    {
        void SignIn(AuthenticationViewModel authenticationVM);
        bool IsAuthenticated();
        void SignOut();
        string GetCurrentUserEmail();
        string GetCurrentUserPhoneNumber();
        long GetCurrentUserId();
        long GetCurrentUserRole();
        string GetCurrentUserAvatarName();
        List<int> GetCurrentUserPermissions();
        void SetUserFullNameCookie(string fullName);
        void SetPhoneNumberCookie(string phoneNumber);
    }
}
