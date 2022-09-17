
using _01_Framework.Application;
using _01_Framework.Infrastructure.ExtensionMethods;
using _01_Framework.Application.Email;
using _01_Framework.Application.ZarinPal;
using FoolProof.Core;
using Microsoft.AspNetCore.Authentication.Cookies;
using Server;
using ShopManagement.Infrastructure.Configuration;
using UserManagement.Infrastructure.Configuration;
using DiscountManagement.Infrastructure.Configuration;

var webApplicationoptions = new WebApplicationOptions()
{
    EnvironmentName = Microsoft.Extensions.Hosting.Environments.Development
};
var builder = WebApplication.CreateBuilder(webApplicationoptions);

// Add services to the container.

#region BootStrapper

UserManagementBootstrapper.Config(builder.Services, builder.Configuration.GetConnectionString("DigikalaConnection"));
ShopManagementBootstrapper.Config(builder.Services,builder.Configuration.GetConnectionString("DigikalaConnection"));
DiscountManagentBootstrapper.Config(builder.Services, builder.Configuration.GetConnectionString("DigikalaConnection"));
#endregion

#region IOC
 
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IViewRenderService,RenderViewToString>();
builder.Services.AddScoped<IAuthenticationHelper,AuthenticationHelper>();
builder.Services.AddScoped<IPasswordHasher,PasswordHasher>();
builder.Services.AddScoped<IZarinpalFactory, ZarinpalFactory>();

#endregion

#region Authentication

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/RegisterAndLogin";
        options.LogoutPath = "/Account/RegisterAndLogin/Logout";
        options.ExpireTimeSpan = TimeSpan.FromDays(14);
        
    });
#endregion

#region Session

builder.Services.AddSession(options =>
    options.IdleTimeout=TimeSpan.FromHours(1)
);

#endregion

builder.Services.AddRazorPages().AddMvcOptions(options =>
    options.Filters.Add<SecurityPageFilter>());

builder.Services.AddAntiforgery(o => o.HeaderName="XSRF-TOKEN");
builder.Services.AddFoolProof();
var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseCultureCookie();

app.UseAuthentication();

app.UseAuthorization();

app.UseSession();

app.MapRazorPages();

app.Run();
