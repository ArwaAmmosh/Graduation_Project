using Graduation_Project.Mapping.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Configuration;
using System.Reflection;

namespace Graduation_Project.Services.Implemention
{
    public static class ServiceRegisteration
    {
        public static IServiceCollection AddServiceRegisteration(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddIdentity<User, IdentityRole<int>>(option =>
            {
                //signin
                option.SignIn.RequireConfirmedEmail = false;
                //user
                option.User.RequireUniqueEmail = true;

                //password
                option.Password.RequireDigit = true;
                option.Password.RequiredLength = 9;
                option.Password.RequiredUniqueChars = 1;
                option.Password.RequireLowercase = true;

                //lockout
                option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                option.Lockout.MaxFailedAccessAttempts = 5;
                option.Lockout.AllowedForNewUsers = true;

            }).AddEntityFrameworkStores<UNITOOLDbContext>().AddDefaultTokenProviders();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(typeof(MappingProfile));
            //JWT
            var jwtSettings = new JWTSettings();
            configuration.GetSection(nameof(jwtSettings)).Bind(jwtSettings);
            services.AddSingleton(jwtSettings);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = jwtSettings.ValidateIssuer,
                    ValidIssuers = new[] { jwtSettings.Issuer },
                    ValidateAudience = jwtSettings.ValidateAudience,
                    ValidAudience = jwtSettings.Audience,
                    ValidateLifetime = jwtSettings.ValidationLifeTime,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Issuer)),
                    ValidateIssuerSigningKey = jwtSettings.ValidationIssuerSigningKey
                };
            });
            
            return services;
        }
    }
}
