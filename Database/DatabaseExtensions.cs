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

            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
