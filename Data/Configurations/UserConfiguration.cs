using KaanBoard.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KaanBoard.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User<Guid>>, IEntityDatabaseConfiguration
    {
        public void Configure(EntityTypeBuilder<User<Guid>> builder)
        {
            builder.HasKey(t => t.Id);

            builder.HasIndex(t => t.UserName).IsUnique();

            //builder.Property(t => t.Id).HasDefaultValueSql("uuidv7()");
            builder.Property(t => t.Id).HasDefaultValueSql("NEWID()");
            builder.Property(t => t.FlAtivo).HasDefaultValue(true);
        }
    }
}
