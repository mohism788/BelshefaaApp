using BelshefaaApp.Core.Interfaces;
using BelshefaaApp.Database.Data;
using BelshefaaApp.Database.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BelshefaaApp.Database
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddDatabaseLayer(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' is missing.");

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<ICarerRepository, CarerRepository>();
            services.AddScoped<IMedicationRepository, MedicationRepository>();
            services.AddScoped<IActivityLogRepository, ActivityLogRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
