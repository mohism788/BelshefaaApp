using BelshefaaApp.Core.Interfaces;
using BelshefaaApp.Database.Data;

namespace BelshefaaApp.Database.Repositories
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(
            AppDbContext context,
            IAccountRepository accounts,
            IPatientRepository patients,
            ICarerRepository carers,
            IMedicationRepository medications,
            IActivityLogRepository activityLogs)
        {
            _context = context;
            Accounts = accounts;
            Patients = patients;
            Carers = carers;
            Medications = medications;
            ActivityLogs = activityLogs;
        }

        public IAccountRepository Accounts { get; }

        public IPatientRepository Patients { get; }

        public ICarerRepository Carers { get; }

        public IMedicationRepository Medications { get; }

        public IActivityLogRepository ActivityLogs { get; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
