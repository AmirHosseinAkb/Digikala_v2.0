using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace _01_Framework.Infrastructure.Middlewares
{
    public class CultureCookieHandlerMiddleware
    {
        private readonly RequestDelegate Next;
        public CultureCookieHandlerMiddleware(RequestDelegate next)
        {
            Next=next;
        }

        public void ChangeCulture(string cultureName)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            ChangeCulture("fa-IR");
            await Next(httpContext);
        }
        
    }
}
