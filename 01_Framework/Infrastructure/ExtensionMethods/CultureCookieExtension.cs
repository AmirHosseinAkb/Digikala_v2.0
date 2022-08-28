using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace _01_Framework.Infrastructure.ExtensionMethods
{
    public static class CultureCookieExtension
    {
        public static void UseCultureCookie(this IApplicationBuilder app) =>
            app.UseMiddleware<CultureCookieHandlerMiddleware>();
    }
}
