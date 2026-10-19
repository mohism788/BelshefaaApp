using BelshefaaApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BelshefaaApp.Database.Data.Configurations
{
    public class MedicationConfiguration : IEntityTypeConfiguration<Medication>
    {
        public void Configure(EntityTypeBuilder<Medication> builder)
        {
            builder.ToTable("Medications");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(m => m.Description)
                .HasMaxLength(1000);

            builder.HasOne(m => m.Account)
                .WithMany()
                .HasForeignKey(m => m.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(m => new { m.AccountId, m.Name })
                .IsUnique();
        }
    }
}
