namespace BelshefaaApp.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountRepository Accounts { get; }

        IPatientRepository Patients { get; }

        ICarerRepository Carers { get; }

        IMedicationRepository Medications { get; }

        IActivityLogRepository ActivityLogs { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
