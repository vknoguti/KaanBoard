using KaanBoard.Models.entities;
using KaanBoard.Models.Entities;
using KaanBoard.Models.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;

namespace KaanBoard.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region ApplicationUser
            //DEIXAR ESSE QUANDO FOR USAR O POSTGRESQL
            //builder.Entity<ApplicationUser>()
            //    .Property(p => p.Id)
            //    .HasDefaultValueSql<Guid>("uuidv7()");

            builder.Entity<ApplicationUser>()
               .Property(p => p.Id)
               .HasDefaultValueSql<Guid>("NEWID()");

            //COLUNA EMAIL
            //builder.Entity<ApplicationUser>()
            //    .Property(p => p.Email)
            //    .HasColumnType("varchar(60)")
            //    .IsRequired()
            //    .HasColumnName("tx_email");

            builder.Entity<ApplicationUser>()
                .Property(p => p.FlAtivo)
                .HasDefaultValue(true);

            builder.Entity<ApplicationUser>()
                .Ignore(p => p.NormalizedEmail);

            builder.Entity<ApplicationUser>()
                .Ignore(p => p.NormalizedUserName);

            #endregion

            #region ApplicationRole
            builder.Entity<ApplicationRole>()
                .Property(p => p.Id)
                .HasDefaultValueSql<Guid>("NEWID()");

            //DEIXAR ESSE QUANDO FOR USAR O POSTGRESQL
            //builder.Entity<ApplicationRole>()
            //    .Property(p => p.Id)
            //    .HasDefaultValueSql<Guid>("uuidv7()");
            #endregion

            #region Board

            builder.Entity<Board>()
                .HasKey(b => b.IdBoard);

            //DEIXAR ESSE QUANDO FOR USAR O POSTGRESQL
            //builder.Entity<Board>()
            //    .Property(p => p.ID_BOARD)
            //    .HasDefaultValueSql<Guid>("uuidv7()");
            builder.Entity<Board>()
                .Property(p => p.IdBoard)
                .HasDefaultValueSql<Guid>("NEWID()");

            #endregion

            #region User_Board
            builder.Entity<UserBoard>()
                .HasKey(nameof(UserBoard.IdUser), nameof(UserBoard.IdBoard));

            builder.Entity<UserBoard>()
                .HasOne(ub => ub.User)
                .WithMany(u => u.UserBoards)
                .HasForeignKey(ub => ub.IdUser);

            builder.Entity<UserBoard>()
                .HasOne(ub => ub.Board)
                .WithMany(b => b.UserBoards)
                .HasForeignKey(b => b.IdBoard);
            #endregion

            #region Columns
            builder.Entity<Models.Entities.Column>()
                .HasKey(c => c.IdColumn);

            //POSTGRESQL
            //builder.Entity<Models.Entities.Column>()
            //   .Property(c => c.IdColumn)
            //   .HasDefaultValueSql<Guid>("uuidv7()");

            builder.Entity<Models.Entities.Column>()
                .Property(c => c.IdColumn)
                .HasDefaultValueSql<Guid>("NEWID()");

            builder.Entity<Models.Entities.Column>()
                .HasOne(c => c.Board)
                .WithMany(b => b.Columns)
                .HasForeignKey(c => c.IdBoard);

            #endregion

            #region TaskItem
            builder.Entity<TaskItem>()
                .HasKey(ti => ti.IdTaskItem);

            builder.Entity<TaskItem>()
                .HasOne(ti => ti.Column)
                .WithMany(c => c.TaskItem)
                .HasForeignKey(ti => ti.IdColumn);
            #endregion
        }
    }
}
