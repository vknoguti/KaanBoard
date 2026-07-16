using KaanBoard.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KaanBoard.Data.Configurations
{
    public class TaskItemUserHistoryConfiguration : IEntityTypeConfiguration<TaskItemUserHistory>, IEntityDatabaseConfiguration
    {
        public void Configure(EntityTypeBuilder<TaskItemUserHistory> builder)
        {
            builder.HasKey(th => new { th.Iduser, th.IdTaskItem });

            //POSTGRESQL
            //builder.Property(th => th.ActionDate).HasDefaultValueSql("NOW()");
            builder.Property(th => th.ActionDate).HasDefaultValueSql("GETDATE()");

            builder.HasOne(th => th.User)
                .WithMany(u => u.TaskItemHistory)
                .HasForeignKey(th => th.Iduser);

            builder.HasOne(th => th.TaskItem)
                .WithMany(u => u.TaskItemHistory)
                .HasForeignKey(th => th.IdTaskItem);
        }
    }
}
