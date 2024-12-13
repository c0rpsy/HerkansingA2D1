using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using HerkansingA2D1.Data;
using System.Globalization;
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
