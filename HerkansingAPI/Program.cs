using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using HerkansingAPI.Data;
using System.Globalization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using HerkansingAPI.Models;
using Microsoft.OpenApi.Models;

namespace HerkansingAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<HerkansingAPIContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("HerkansingA2D1Context") ?? throw new InvalidOperationException("Connection string 'HerkansingA2D1Context' not found.")));

            // Add services to the container.
            builder.Services.AddControllers();

            // Add Identity services
            builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<HerkansingAPIContext>()
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

            // Add Swagger services
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HerkansingAPI", Version = "v1" });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Enable authentication and authorization middleware
            app.UseAuthentication();
            app.UseAuthorization();

            // Enable Swagger middleware
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "HerkansingAPI v1");
                c.RoutePrefix = "swagger"; // Set Swagger UI at /swagger
            });

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

            app.MapControllers();

            app.Run();
        }
    }
}
