using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using KingsStoreApi.Model.Entities;
using KingsStoreApi.Model.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace KingsStoreApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void SetupServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddDbContext<KingsStoreContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("StoreConnection"), b => b.MigrationsAssembly("KingsStoreApi"));
            });

            services.AddIdentity<User, Role>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredUniqueChars = 1;
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.Parse("5");

            }).AddEntityFrameworkStores<KingsStoreContext>().AddDefaultTokenProviders();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminRolePolicy", p => p.RequireRole(Roles.Admin.ToString()));
                options.AddPolicy("CustomerRolePolicy", p => p.RequireRole(Roles.Customer.ToString()));
                options.AddPolicy("VendorRolePolicy", p => p.RequireRole(Roles.Vendor.ToString()));
            });
        }

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = configuration["SecretKey"];

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(option =>
                {
                    option.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,

                        ValidAudience = jwtSettings.GetSection("ValidAudience").Value,
                        ValidIssuer = jwtSettings.GetSection("ValidIssuer").Value,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                    };

                });
        }

        public static (string userId, string userEmail) GetLoggedInUserInfo (this ClaimsPrincipal user)
        {
            var userId = user.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Name, StringComparison.InvariantCulture)).Value;
            var userEmail = user.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Email, StringComparison.InvariantCulture)).Value;
            
            return (userId, userEmail);
        }
    }
}
