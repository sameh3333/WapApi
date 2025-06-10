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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using WapApi.Models;

namespace WapApi.Services
{
    public class RegisterServciesHelper 
    {
         public static void RegisteredServices( WebApplicationBuilder builder)
        {



            #region Instole


            builder.Services.AddControllers();
            var jwtSettingsSection = builder.Configuration.GetSection("JwtSettings");
            var jwtSettings = jwtSettingsSection.Get<JwtSettings>();
            var key = Encoding.UTF8.GetBytes(jwtSettings.Key);
            builder.Services.AddHttpClient();


            // Bind JwtSettings from appsettings.json
            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));



            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

          

            #endregion




            //builder.Services.AddAuthorization(CookieAuthenticationDefaults.AuthenticationScheme
            //    .AddC)
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
      .AddCookie(options =>
      {
          options.LoginPath = "/login";
          options.AccessDeniedPath = "/access-denied";
      });
            #region Entity Framework

            builder.Services.AddDbContext<ShippingContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            #region Identity

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = true;

            }).AddEntityFrameworkStores<ShippingContext>();

            #endregion
            #endregion


            #region Sesstion and cookies

            //builder.Services.ConfigureApplicationCookie(options =>
            //{
            //    options.LoginPath = "/Account/Login";

            //    options.AccessDeniedPath = "/Account/AccessDenied";
            //    options.Cookie.Name = "Cookie";
            //    options.Cookie.HttpOnly = true;
            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(720);
            //   // options.LoginPath = "/Accuent/Register";
            //    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            //    options.SlidingExpiration = true;
            //});
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
            builder.Services.AddScoped<ICairrer, CairrerServices>();
            builder.Services.AddScoped<ICity, CityServices>();
            builder.Services.AddScoped<ICountry, CountryServices>();
            builder.Services.AddScoped<IPaymentMethod, PaymentMethodServices>();
            builder.Services.AddScoped<ISetting, SettingServices>();
            builder.Services.AddScoped<IShippmentStatus, ShippmentStatusServices>();
            //Shiping Services 
            builder.Services.AddScoped<IShippment, ShippmentServices>();

            builder.Services.AddScoped<ITrackingNumberCreator, TrackingNumberCretatorServices>();
            builder.Services.AddScoped<IRateCalculator, RateCalculatorServices>();

            builder.Services.AddScoped<ISubscriptionPackage, SubscriptionPackageServices>();
            builder.Services.AddScoped<IUserReceiver, UserReceiverServices>();
            builder.Services.AddScoped<IUserSebder, UserSebderSerivces>();
            builder.Services.AddScoped<IUserSubscription, UserSubscriptionServices>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IRefreshTokens, RefreshTokenServices>();
            builder.Services.AddSingleton<TokinService>();
            builder.Services.AddScoped<GenericApiClient>();
            #endregion

        }

    }
}
