using BelshefaaApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BelshefaaApp.Database.Data.Configurations
{
    public class ActivityLogConfiguration : IEntityTypeConfiguration<ActivityLog>
    {
        public void Configure(EntityTypeBuilder<ActivityLog> builder)
        {
            builder.ToTable("ActivityLogs");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Action)
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(l => l.Details)
                .HasMaxLength(1000);

            builder.HasOne(l => l.Account)
                .WithMany()
                .HasForeignKey(l => l.AccountId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(l => l.Patient)
                .WithMany()
                .HasForeignKey(l => l.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(l => l.Carer)
                .WithMany()
                .HasForeignKey(l => l.CarerId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(l => l.Medication)
                .WithMany()
                .HasForeignKey(l => l.MedicationId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasIndex(l => l.PatientId);

            builder.HasIndex(l => l.AccountId);

            builder.HasIndex(l => l.CreatedAt);
        }
    }
}
