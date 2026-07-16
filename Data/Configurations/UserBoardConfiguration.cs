using KaanBoard.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KaanBoard.Data.Configurations
{
    public class UserBoardConfiguration : IEntityTypeConfiguration<UserBoard>, IEntityDatabaseConfiguration
    {
        public void Configure(EntityTypeBuilder<UserBoard> builder)
        {
            builder.HasKey(t => new { t.IdUser, t.IdBoard });

            builder.HasOne(ub => ub.User)
                .WithMany(u => u.UserBoards)
                .HasForeignKey(ub => ub.IdUser);
            builder.HasOne(ub => ub.Board)
                .WithMany(b => b.UserBoards)
                .HasForeignKey(b => b.IdBoard);
        }
    }
}
