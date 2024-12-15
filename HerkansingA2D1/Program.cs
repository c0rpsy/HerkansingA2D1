using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using HerkansingA2D1.Data;
using System.Globalization;
using Microsoft.AspNetCore.Authentication.Cookies;
using HerkansingA2D1.Services;
using Microsoft.Extensions.Logging;

namespace HerkansingA2D1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add logging to trace the build process
            var logger = LoggerFactory.Create(logging => logging.AddConsole()).CreateLogger<Program>();
            logger.LogInformation("Starting application build process.");

            builder.Services.AddDbContext<HerkansingA2D1Context>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("HerkansingA2D1Context") ?? throw new InvalidOperationException("Connection string 'HerkansingA2D1Context' not found.")));

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            // Register ICartService
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<ICartService, CartService>();

            // Add session services
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            // Add authentication services
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/AppUsers/Login";
                });

            // Add authorization policies
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("OwnerOnly", policy => policy.RequireRole("Owner"));
            });

            logger.LogInformation("Building the application.");
            var app = builder.Build();
            logger.LogInformation("Application build completed.");

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Enable session middleware
            app.UseSession();

            // Enable authentication and authorization middleware
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            var cultureInfo = new CultureInfo("en-US")
            {
                NumberFormat = {
                    CurrencyDecimalSeparator = ".",
                    NumberDecimalSeparator = "."
                }
            };
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            app.Run();
        }
    }
}
