using KaanBoard.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KaanBoard.Data.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>, IEntityDatabaseConfiguration
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(c => c.IdComment);

            //POSTGRESQL
            //builder.Property(c => c.DtCreation)
            //  .HasDefaultValueSql("NOW()");
            builder.Property(c => c.DtCreation)
                .HasDefaultValueSql("GETDATE()");

            builder.Property(c => c.IsDeleted)
                .HasDefaultValue(false);

            builder.HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.IdUser);
            builder.HasOne(c => c.TaskItem)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.IdTaskItem);
        }
    }
}
