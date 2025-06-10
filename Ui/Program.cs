using BL.Contracts;
using BL.Maping;
using BL.Services;
using DAL.Contracts;
using DAL.Models;
using DAL.Repositorys;
using Domines;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;
using Serilog;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Identity;
using Ui.Services;

namespace Ui
{
    public class Program
    {
        public static async Task Main(string[] args)
        {



            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.WithOrigins("http://localhost:5125/") // ÑÇÈØ ãÔÑæÚ ÇáÜ UI
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials();
                });
            });

            builder.Services.AddControllersWithViews();
          
            RegisterServciesHelper.RegisteredServices(builder);
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStaticFiles();

            app.UseRouting();
            app.UseHttpsRedirection();

            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();

           
            #region Routing
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "admin",
                pattern: "{area:exists}/{controller=Home}/{action=Index}");

                endpoints.MapControllerRoute(
                name: "LandingPages",
                pattern: "{area:exists}/{controller=Home}/{action=Index}");

                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                name: "admin",
                pattern: "{controller=Home}/{action=List}/{id?}");

                endpoints.MapControllerRoute(
  "areas",
  "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

            }
            );
            #endregion




            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var userManger = services.GetRequiredService<UserManager<ApplicationUser>>();
                var rloeManger = services.GetRequiredService<RoleManager<IdentityRole>>();
                var dbContxt = services.GetRequiredService<ShippingContext>();
                await dbContxt.Database.MigrateAsync();
                await ContextConfig.SeedDataAsync(dbContxt, userManger, rloeManger);
            }
            await app.RunAsync();

            app.Run();
        }
    }
}
