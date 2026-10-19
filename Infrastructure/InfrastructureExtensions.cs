using AutoMapper;
using BelshefaaApp.Core.Interfaces;
using BelshefaaApp.Infrastructure.Mappings;
using BelshefaaApp.Infrastructure.Services;

namespace BelshefaaApp.Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile).Assembly);

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IActivityLogService, ActivityLogService>();
            services.AddScoped<ICarerService, CarerService>();
            services.AddScoped<IMedicationService, MedicationService>();

            return services;
        }
    }
}
