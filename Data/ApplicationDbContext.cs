using KaanBoard.Models;
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

            #region TaskItemUserHistory
            builder.Entity<TaskItemUserHistory>()
                .HasKey(th => new { th.Iduser, th.IdTaskItem });

            //POSTGRESQL
            //builder.Entity<TaskItemUserHistory>()
            //    .Property(th => th.ActionDate)
            //    .HasDefaultValueSql("NOW()");

            builder.Entity<TaskItemUserHistory>()
                .Property(th => th.ActionDate)
                .HasDefaultValueSql("GETDATE()");

            builder.Entity<TaskItemUserHistory>()
                .HasOne(th => th.User)
                .WithMany(u => u.TaskItemHistory)
                .HasForeignKey(th => th.Iduser);

            builder.Entity<TaskItemUserHistory>()
             .HasOne(th => th.TaskItem)
             .WithMany(u => u.TaskItemHistory)
             .HasForeignKey(th => th.IdTaskItem);
            #endregion

            #region Comment
            builder.Entity<Comment>()
                .HasKey(c => c.IdComment);

            //POSTGRESQL
            //builder.Entity<Comment>()
            //    .Property(c => c.DtCreation)
            //    .HasDefaultValueSql("NOW()");

            builder.Entity<Comment>()
                .Property(c => c.DtCreation)
                .HasDefaultValueSql("GETDATE()");

            builder.Entity<Comment>()
                .Property(c => c.IsDeleted)
                .HasDefaultValue(false);

            builder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.IdUser);

            builder.Entity<Comment>()
                .HasOne(c => c.TaskItem)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.IdTaskItem);
            #endregion

            #region CommentHistory
            builder.Entity<CommentHistory>()
                .HasKey(ch => ch.IdCommentHistory);

            //DEIXAR ESSE QUANDO FOR USAR O POSTGRESQL
            //builder.Entity<CommentHistory>()
            //    .Property(ch => ch.IdCommentHistory)
            //    .HasDefaultValueSql("uuidv7()");

            builder.Entity<CommentHistory>()
                .Property(ch => ch.IdCommentHistory)
                .HasDefaultValueSql("NEWID()");

            #endregion
        }
    }
}
