using KaanBoard.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KaanBoard.Data.Configurations
{
    public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>, IEntityDatabaseConfiguration
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            builder.HasKey(t => t.IdTaskItem);

            builder.HasOne(ti => ti.Column)
                .WithMany(c => c.TaskItem)
                .HasForeignKey(ti => ti.IdColumn);
        }
    }
}
