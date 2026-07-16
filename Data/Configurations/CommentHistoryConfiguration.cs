using KaanBoard.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KaanBoard.Data.Configurations
{
    public class CommentHistoryConfiguration : IEntityTypeConfiguration<CommentHistory>, IEntityDatabaseConfiguration
    {
        public void Configure(EntityTypeBuilder<CommentHistory> builder)
        {
            builder.HasKey(ch => ch.IdCommentHistory);

            //POSTGRESQL
            //builder.Property(ch => ch.IdCommentHistory).HasDefaultValueSql("uuidv7()");
            builder.Property(ch => ch.IdCommentHistory).HasDefaultValueSql("NEWID()");
        }
    }
}
