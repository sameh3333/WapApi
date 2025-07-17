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
using Microsoft.IdentityModel.Tokens;
using Serilog;
using WapApi.Models;
using System.Text;
using WapApi.Models;

namespace WapApi.Services
{
    public class RegisterServciesHelper
    {
        private readonly IConfiguration _configuration;

        public RegisterServciesHelper(IConfiguration configuration)
        {
            // this.configuration = configuration;
            this._configuration = configuration;
        }



        public static void RegisteredServices(WebApplicationBuilder builder)
        {



        // Add MVC Controllers
        builder.Services.AddControllers();

            // Cookie Authentication (can be used for Admin panel)
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/login";
                    options.AccessDeniedPath = "/access-denied";
                });

            // Database context
            builder.Services.AddDbContext<ShippingContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            #region Identity Configuration
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ShippingContext>()
            .AddDefaultTokenProviders();
            #endregion

            #region JWT Configuration

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = true;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    // ValidIssuer = configuration["JWT:VaildIssure"], // If this line were active, it would also need builder.Configuration
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:VaildAddience"], // Corrected: Use builder.Configuration
                                                                                //SecurityKey securityKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:SigningKey"]));
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"])) // Corrected: Use builder.Configuration

                };

            });
            #endregion

            #region Serilog Logging
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

            #region Register Services
            builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped(typeof(IViewRepository<>), typeof(ViewRepository<>));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Shipment Services
            builder.Services.AddScoped<IShippmentType, ShippmentYTypeServices>();
            builder.Services.AddScoped<IShipingPackging, ShippingPackgingServices>();
            builder.Services.AddScoped<ICairrer, CairrerServices>();
            builder.Services.AddScoped<ICity, CityServices>();
            builder.Services.AddScoped<ICountry, CountryServices>();
            builder.Services.AddScoped<IPaymentMethod, PaymentMethodServices>();
            builder.Services.AddScoped<ISetting, SettingServices>();
            builder.Services.AddScoped<IShippmentStatus, ShippmentStatusServices>();
            builder.Services.AddScoped<IShippment, ShippmentServices>();

            // Tracking and Rate Services
            builder.Services.AddScoped<ITrackingNumberCreator, TrackingNumberCretatorServices>();
            builder.Services.AddScoped<IRateCalculator, RateCalculatorServices>();

            // User and Token Services
            builder.Services.AddSingleton<TokinService>();

            builder.Services.AddScoped<ISubscriptionPackage, SubscriptionPackageServices>();
            builder.Services.AddScoped<IUserReceiver, UserReceiverServices>();
            builder.Services.AddScoped<IUserSebder, UserSebderSerivces>();
            builder.Services.AddScoped<IUserSubscription, UserSubscriptionServices>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IRefreshTokens, RefreshTokenServices>();
            builder.Services.AddScoped<IRefreshTokensRetriver, RefreshTokensRetriverServices>();
          //  builder.Services.AddScoped<TokinService>(); // Your JWT service
            builder.Services.AddScoped<GenericApiClient>(); // For calling API from UI
            #endregion
        }
    }
}
