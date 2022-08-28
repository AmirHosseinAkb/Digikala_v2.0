using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace _01_Framework.Application
{
    public class AuthenticationHelper:IAuthenticationHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticationHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public void SignIn(AuthenticationViewModel authenticationVM)
        {
            var permissions = JsonConvert.SerializeObject(authenticationVM.Permissions);
            var claims = new List<Claim>()
            {
                new Claim("UserId", authenticationVM.UserId.ToString()),
                new Claim(ClaimTypes.Role, authenticationVM.RoleId.ToString()),
                new Claim("Permissions",permissions),
                new Claim("AvatarName",authenticationVM.AvatarName)
            };

            if(!string.IsNullOrEmpty(authenticationVM.Email))
                claims.Add(new Claim(ClaimTypes.Email, authenticationVM.Email));

            if(!string.IsNullOrEmpty(authenticationVM.PhoneNumber))
                claims.Add(new Claim(ClaimTypes.MobilePhone,authenticationVM.PhoneNumber));

            var identity=new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties()
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(14)
            };
            _httpContextAccessor.HttpContext.SignInAsync(principal, properties);
        }

        public bool IsAuthenticated()
        {
            return _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public void SignOut()
        {
            _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public string GetCurrentUserEmail()
        {
            return IsAuthenticated()
                ? _httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Email).Value
                : "";
        }

        public string GetCurrentUserPhoneNumber()
        {
            return IsAuthenticated()
                ? _httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == ClaimTypes.MobilePhone).Value
                : "";
        }

        public long GetCurrentUserId()
        {
            return IsAuthenticated()
                ? long.Parse(_httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == "UserId").Value)
                : 0;
        }

        public long GetCurrentUserRole()
        {
            return IsAuthenticated()
                ? long.Parse(_httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Role).Value)
                : 0;
        }

        public string GetCurrentUserAvatarName()
        {
            return IsAuthenticated()
                ? _httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == "AvatarName").Value
                : "";
        }

        public List<int> GetCurrentUserPermissions()
        {
            if (!IsAuthenticated())
                return new List<int>();
            var permissions=JsonConvert.DeserializeObject<List<int>>(_httpContextAccessor.HttpContext.User.Claims?.FirstOrDefault(c => c.Type == "Permissions").Value);
            return permissions;
        }

    }
}
