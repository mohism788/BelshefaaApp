using BelshefaaApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BelshefaaApp.Database.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Account> Accounts => Set<Account>();

        public DbSet<Patient> Patients => Set<Patient>();

        public DbSet<Carer> Carers => Set<Carer>();

        public DbSet<Medication> Medications => Set<Medication>();

        public DbSet<ActivityLog> ActivityLogs => Set<ActivityLog>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
