using KaanBoard.Data.Configurations;
using KaanBoard.Entities;
using KaanBoard.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KaanBoard.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser<Guid>, ApplicationRole<Guid>, Guid> 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ApplicationRole<Guid>> AspNetRoles { get; set; }
        public DbSet<ApplicationUser<Guid>> AspNetUsers { get; set; }
        public DbSet<Board> Boards { get; set;  }
        public DbSet<Column> Columns { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentHistory> CommentHistory { get; set; }
        public DbSet<TaskItem> TaskItem { get; set; }
        public DbSet<TaskItemUserHistory> TaskItemUserHistory { get; set; }
        public DbSet<UserBoard> UserBoard { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(
                assembly: typeof(ApplicationDbContext).Assembly,
                predicate: type => typeof(IEntityDatabaseConfiguration).IsAssignableFrom(type)
            );
        }
    }
}
