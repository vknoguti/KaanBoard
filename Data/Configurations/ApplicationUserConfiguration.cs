using KaanBoard.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KaanBoard.Data.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser<Guid>>, IEntityDatabaseConfiguration
    {
        public void Configure(EntityTypeBuilder<ApplicationUser<Guid>> builder)
        {
            builder.HasKey(t => t.Id);
            //builder.Property(t => t.Id).HasDefaultValueSql("uuidv7()");
            builder.Property(t => t.Id).HasDefaultValueSql("NEWID()");
            builder.Property(t => t.FlAtivo).HasDefaultValue(true);

            builder.Ignore(t => t.NormalizedUserName);
            builder.Ignore(t => t.NormalizedEmail);
            builder.Ignore(t => t.EmailConfirmed);
            builder.Ignore(t => t.SecurityStamp);
            builder.Ignore(t => t.ConcurrencyStamp);
            builder.Ignore(t => t.PhoneNumberConfirmed);
            builder.Ignore(t => t.TwoFactorEnabled);
            builder.Ignore(t => t.LockoutEnd);
            builder.Ignore(t => t.LockoutEnabled);
            builder.Ignore(t => t.AccessFailedCount);
        }
    }
}
