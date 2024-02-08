using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Identity.Web;
using MRF.DataAccess.Repository.IRepository;
using MRF.DataAccess.Repository;
using MRF.Utility;
using MRF.DataAccess.Data;
using Microsoft.EntityFrameworkCore; // Ensure this namespace is included
using Microsoft.AspNetCore.Http; // Ensure this namespace is included

namespace MRF.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var environment = builder.Environment.EnvironmentName;
            var config = new ConfigurationBuilder()
                   .AddJsonFile("appsettings." + environment + ".json", optional: false)
                   .Build();
            builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApp(builder.Configuration);

            builder.Services.AddHttpContextAccessor(); // Register IHttpContextAccessor here

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddDbContext<MRFDBContext>(options =>
         options.UseMySql(config.GetConnectionString("DbConnectionString"), ServerVersion.AutoDetect(config.GetConnectionString("DbConnectionString")))
             .EnableDetailedErrors()
             .EnableSensitiveDataLogging());

            // Add services to the container.
            builder.Services.AddControllersWithViews(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); // Register IUnitOfWork here
            builder.Services.AddScoped<ILoggerService, LoggerService>();
            builder.Services.AddScoped<IEmailService, EmailService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
