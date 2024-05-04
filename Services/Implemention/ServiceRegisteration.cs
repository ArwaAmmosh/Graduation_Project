using Graduation_Project.Entities.Identity;
using Graduation_Project.Mapping.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Security.Claims;

namespace Graduation_Project.Services
{
    public static class ServiceRegisteration
    {
        public static IServiceCollection AddServiceRegisteration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<User, Role>(option =>
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
            //JWT Authentication
            var jwtSettings = new JwtSettings();
            configuration.GetSection(nameof(jwtSettings)).Bind(jwtSettings);
            services.AddSingleton(jwtSettings);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = jwtSettings.ValidateIssuer,
                    ValidIssuers = new[] { jwtSettings.Issuer },
                    ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                    ValidAudience = jwtSettings.Audience,
                    ValidateAudience = jwtSettings.ValidateAudience,
                    ValidateLifetime = jwtSettings.ValidateLifeTime,
                    NameClaimType = ClaimTypes.NameIdentifier
                };
            });




            //Swagger Gn
            services.AddSwaggerGen(c =>
             {
                 c.SwaggerDoc("v1", new OpenApiInfo { Title = "Graduation Project", Version = "v1" });
                 c.EnableAnnotations();

                 c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                 {
                     Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                     Name = "Authorization",
                     In = ParameterLocation.Header,
                     Type = SecuritySchemeType.ApiKey,
                     Scheme = JwtBearerDefaults.AuthenticationScheme
                 });

                 c.AddSecurityRequirement(new OpenApiSecurityRequirement
             {
{
             new OpenApiSecurityScheme
    {
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = JwtBearerDefaults.AuthenticationScheme
        }
    },
    Array.Empty<string>()
    }
            });
             });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Registeriation", policy =>
                {
                    policy.RequireClaim("Registeriation", "true");
                }
                );
            }
            );

            return services;

        }
    } }

