﻿using Graduation_Project.Entities.Identity;
using Microsoft.AspNetCore.Identity;

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
                option.Password.RequireUppercase = true;

                //lockout
                option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                option.Lockout.MaxFailedAccessAttempts = 5;
                option.Lockout.AllowedForNewUsers = true;

            }).AddEntityFrameworkStores<UNITOOLDbContext>().AddDefaultTokenProviders();
            return services;
        }
    }
}