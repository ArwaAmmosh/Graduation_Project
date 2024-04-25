
namespace Graduation_Project.Service
{
    public static class ModuleServiceDependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {

            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IUserService, UserServices>();
            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddTransient<IFileService, FileService>();
            return services;
        }
    }
}