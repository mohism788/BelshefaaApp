using BelshefaaApp.Core.Interfaces;
using BelshefaaApp.Infrastructure.Services;

namespace BelshefaaApp.Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPatientService, PatientService>();

            return services;
        }
    }
}
