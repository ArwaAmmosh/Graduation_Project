using Graduation_Project.Entities.Identity;
using Graduation_Project.Mapping.Users;
using Microsoft.AspNetCore.Identity;
using System.Reflection;

namespace Graduation_Project.Services
{
    public static class ServiceRegisteration
    {
        public static IServiceCollection AddServiceRegisteration(this IServiceCollection services)
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
            return services;
        }
    }
}
