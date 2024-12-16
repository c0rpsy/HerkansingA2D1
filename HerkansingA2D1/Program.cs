using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using HerkansingA2D1.Data;
using System.Globalization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using HerkansingA2D1.Models;

namespace HerkansingA2D1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<HerkansingA2D1Context>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("HerkansingA2D1Context") ?? throw new InvalidOperationException("Connection string 'HerkansingA2D1Context' not found.")));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Add Identity services
            builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<HerkansingA2D1Context>()
                .AddDefaultTokenProviders();

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

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Enable authentication and authorization middleware
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // Cultuurinstellingen voor het gebruik van punt als decimaalteken
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
