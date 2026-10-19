using BelshefaaApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BelshefaaApp.Database.Data.Configurations
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patients");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.LastName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.NationalId)
                .HasMaxLength(20);

            builder.Property(p => p.PhoneNumber)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(p => p.Email)
                .HasMaxLength(256);

            builder.Property(p => p.Address)
                .HasMaxLength(500);

            builder.Property(p => p.Notes)
                .HasMaxLength(2000);

            builder.HasOne(p => p.Account)
                .WithMany(a => a.Patients)
                .HasForeignKey(p => p.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(p => new { p.AccountId, p.NationalId })
                .IsUnique()
                .HasFilter("[NationalId] IS NOT NULL");
        }
    }
}
