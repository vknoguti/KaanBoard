using KaanBoard.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KaanBoard.Data.Configurations
{
    public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole<Guid>>, IEntityDatabaseConfiguration
    {
        public void Configure(EntityTypeBuilder<ApplicationRole<Guid>> builder)
        {
            builder.HasKey(t => t.Id);
            //POSTGRESQL
            //builder.Property(t => t.Id).HasDefaultValueSql("uuidv7()");
            builder.Property(t => t.Id).HasDefaultValueSql<Guid>("NEWID()");

            builder.Ignore(t => t.NormalizedName);
            builder.Ignore(t => t.ConcurrencyStamp);
        }
    }
}
