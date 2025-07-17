using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using DAL.Contracts;
using Domines;
using System.Configuration;
using System;
using WapApi.Services;
using WapApi.Models;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;

namespace WapApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.WithOrigins("https://localhost:7034") // 👈 Your MVC project URL
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials(); // 👈 Required for cookies (refresh token)
                });
            });

            // 1. Configure services BEFORE build
            builder.Services.AddControllers()
             .AddJsonOptions(options =>
             {
                 options.JsonSerializerOptions.PropertyNamingPolicy = null;
             });

            builder.Services.AddAuthorization();
            builder.Services.AddHttpClient();





            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            RegisterServciesHelper.RegisteredServices(builder);



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseRouting();
            app.UseCors();
             app.UseHttpsRedirection();
            app.UseCors("AllowAll");
          
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

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
