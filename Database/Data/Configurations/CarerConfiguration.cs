using BelshefaaApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BelshefaaApp.Database.Data.Configurations
{
    public class CarerConfiguration : IEntityTypeConfiguration<Carer>
    {
        public void Configure(EntityTypeBuilder<Carer> builder)
        {
            builder.ToTable("Carers");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.FullName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.PhoneNumber)
                .HasMaxLength(20);

            builder.HasOne(c => c.Account)
                .WithMany()
                .HasForeignKey(c => c.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(c => c.AccountId);
        }
    }
}
