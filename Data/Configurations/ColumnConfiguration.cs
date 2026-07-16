using KaanBoard.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KaanBoard.Data.Configurations
{
    public class ColumnConfiguration : IEntityTypeConfiguration<Column>, IEntityDatabaseConfiguration
    {
        public void Configure(EntityTypeBuilder<Column> builder)
        {
            builder.HasKey(t => t.IdColumn);
            //builder.Property(t => t.IdColumn).HasDefaultValueSql("uuidv7()");
            builder.Property(t => t.IdColumn).HasDefaultValueSql("NEWID()");

            builder.HasOne(c => c.Board)
                .WithMany(b => b.Columns)
                .HasForeignKey(c => c.IdBoard);
        }
    }
}
