using BelshefaaApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BelshefaaApp.Database.Data.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Email)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(a => a.PasswordHash)
                .HasMaxLength(512)
                .IsRequired();

            builder.Property(a => a.FullName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(a => a.Role)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

            builder.HasIndex(a => a.Email)
                .IsUnique();
        }
    }
}
