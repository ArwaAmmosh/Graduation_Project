using Graduation_Project.Services.Implemention;

namespace Graduation_Project.Services
{
    public static class ModuleServiceDependencies
    {
        public static IServiceCollection AddServiceDependencies (this IServiceCollection services)
        {
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            return services;
        }
    }
}
