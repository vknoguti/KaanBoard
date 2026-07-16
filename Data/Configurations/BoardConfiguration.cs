using KaanBoard.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KaanBoard.Data.Configurations
{
    public class BoardConfiguration : IEntityTypeConfiguration<Board>, IEntityDatabaseConfiguration
    {
        public void Configure(EntityTypeBuilder<Board> builder)
        {
            builder.HasKey(t => t.IdBoard);
            //POSTGRESQL
            //builder.Property(t => t.IdBoard).HasDefaultValueSql("uuidv7()");
            builder.Property(t => t.IdBoard).HasDefaultValueSql("NEWID()");
        }
    }
}
