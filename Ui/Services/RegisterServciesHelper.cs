using BL.Contracts;
using BL.Contracts.Shipment;
using BL.Maping;
using BL.Services;
using BL.Services.Shipment;
using DAL.Contracts;
using DAL.Models;
using DAL.Repositorys;
using Domines;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Text;
using WapApi.Models;
using WapApi.Services;


namespace Ui.Services
{
    public class RegisterServciesHelper 
    {
         public static void RegisteredServices( WebApplicationBuilder builder)
        {

            builder.Services.AddControllers();

            builder.Services.AddHttpClient();
            var apiBaseUrl = builder.Configuration["ApiSettings:BaseUrl"];

            // Add HttpContextAccessor (needed for accessing cookies)
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddDbContext<ShippingContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            // Register named HttpClient
            builder.Services.AddHttpClient("ApiClient", client =>
            {
                client.BaseAddress = new Uri(apiBaseUrl);
                client.DefaultRequestHeaders.Add("Accept","application/json");
            });

            builder.Services.AddHttpClient("ApiClient", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7034"); // ده بورت الـ Web API
            });
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
      .AddCookie(options =>
      {
          options.LoginPath = "/login";
          options.AccessDeniedPath = "/access-denied";
          options.SlidingExpiration= true;
          options.Cookie.IsEssential = true;
          options.ExpireTimeSpan = TimeSpan.FromDays(7);



      });
            #region Entity Framework
            builder.Services.AddDbContext<ShippingContext>(options =>
     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            #region Identity

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;

            }).AddEntityFrameworkStores<ShippingContext>();

            #endregion
            #endregion


            #region Sesstion and cookies

        
            #endregion
            #region lOgger Message



            //is using Logger
            Serilog.Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.MSSqlServer(
                    connectionString: builder.Configuration.GetConnectionString("DefaultConnection"),
                    tableName: "Log",
                    autoCreateSqlTable: true)
                .CreateLogger();
            builder.Host.UseSerilog();


            builder.Services.AddLogging(); 
            #endregion

            #region Services
            builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
            //builder.Services.AddScoped<IGenericRepository<TbShippingType>, DAL.Repositorys.GenericRepository<TbShippingType>>();

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped(typeof(IViewRepository<>), typeof(ViewRepository<>));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IShippmentType, ShippmentYTypeServices>();
            builder.Services.AddScoped<IShipingPackging, ShippingPackgingServices>();

            builder.Services.AddScoped<ICairrer, CairrerServices>();
            builder.Services.AddScoped<ICity, CityServices>();
            builder.Services.AddScoped<ICountry, CountryServices>();
            builder.Services.AddScoped<IPaymentMethod, PaymentMethodServices>();
            builder.Services.AddScoped<ISetting, SettingServices>();
            builder.Services.AddScoped<IShippmentStatus, ShippmentStatusServices>();

            builder.Services.AddScoped<IShippment, ShippmentServices>();
            builder.Services.AddScoped<ITrackingNumberCreator, TrackingNumberCretatorServices>();
            builder.Services.AddScoped<IRateCalculator, RateCalculatorServices>();

            builder.Services.AddScoped<ISubscriptionPackage, SubscriptionPackageServices>();
            builder.Services.AddScoped<IUserReceiver, UserReceiverServices>();
            builder.Services.AddScoped<IUserSebder, UserSebderSerivces>();
            builder.Services.AddScoped<IUserSubscription, UserSubscriptionServices>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IRefreshTokens, RefreshTokenServices>();
            builder.Services.AddScoped<TokinService>();
            //      builder.Services.AddScoped<IRefreshTokensRetriver, RefreshTokensRetriverServices>();

            builder.Services.AddScoped<IRefreshTokensRetriver, RefreshTokensRetriverServices>();

            builder.Services.AddScoped<GenericApiClient>();
            #endregion

        }

    }
}
