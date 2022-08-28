using System.Reflection;
using _01_Framework.Application;
using _01_Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Server
{
    public class SecurityPageFilter : IPageFilter
    {
        private readonly IAuthenticationHelper _authenticationHelper;

        public SecurityPageFilter(IAuthenticationHelper authenticationHelper)
        {
            _authenticationHelper = authenticationHelper;
        }
        public void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {

        }

        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            var handlerPermission =
                (NeedsPermissionAttribute?) context.HandlerMethod?.MethodInfo?.GetCustomAttribute(
                    typeof(NeedsPermissionAttribute));

            if (handlerPermission == null)
                return;

            var accountPermissions = _authenticationHelper.GetCurrentUserPermissions();

            if (accountPermissions.All(x => x != handlerPermission.PermissionCode))
                context.HttpContext.Response.Redirect("/Account/RegisterAndLogin");

        }

        public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {

        }
    }
}
