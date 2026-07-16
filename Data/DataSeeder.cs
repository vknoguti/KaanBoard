using KaanBoard.Entities;
using KaanBoard.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KaanBoard.Data
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Usamos o IPasswordHasher diretamente para gerar o PasswordHash sem depender do UserManager/UserStore
            var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher<ApplicationUser<Guid>>>();

            // Opcional: Aplica migrations pendentes
            //await context.Database.MigrateAsync();

            // 1. Popular Roles diretamente via DbContext (apenas colunas mapeadas: Id e Name)
            var roleAdminId = Guid.NewGuid();
            var roleMemberId = Guid.NewGuid();

            if (!await context.Set<ApplicationRole<Guid>>().AnyAsync())
            {
                var roles = new List<ApplicationRole<Guid>>
                {
                    new ApplicationRole<Guid> { Id = roleAdminId, Name = "Admin" },
                    new ApplicationRole<Guid> { Id = roleMemberId, Name = "Member" }
                };
                context.Set<ApplicationRole<Guid>>().AddRange(roles);
                await context.SaveChangesAsync();
            }
            else
            {
                // Se já existirem roles, tentamos pegar o ID da role Admin para vincular o usuário
                var existingRole = await context.Set<ApplicationRole<Guid>>()
                    .FirstOrDefaultAsync(r => r.Name == "Admin");
                if (existingRole != null) roleAdminId = existingRole.Id;
            }

            // 2. Popular Usuário diretamente via DbContext (apenas as propriedades mapeadas no seu ApplicationUser)
            var adminEmail = "admin@kaanboard.com";
            var adminUser = await context.Set<ApplicationUser<Guid>>()
                .FirstOrDefaultAsync(u => u.Email == adminEmail);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser<Guid>
                {
                    Id = Guid.NewGuid(),
                    UserName = "admin",
                    Name = "Administrador do Sistema",
                    Email = adminEmail,
                    CreatedAt = DateTimeOffset.UtcNow,
                    FlAtivo = true
                };

                // Criptografa a senha "Admin@123" usando o hasher nativo do Identity
                adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "Admin@123");

                context.Set<ApplicationUser<Guid>>().Add(adminUser);

                // 3. Vincular o usuário à Role Admin na tabela intermediária (AspNetUserRoles)
                if (roleAdminId != Guid.Empty)
                {
                    context.Set<IdentityUserRole<Guid>>().Add(new IdentityUserRole<Guid>
                    {
                        UserId = adminUser.Id,
                        RoleId = roleAdminId
                    });
                }

                await context.SaveChangesAsync();
            }

            // 4. Popular Quadro (Board), Colunas e Tarefas
            if (!await context.Boards.AnyAsync())
            {
                var boardId = Guid.NewGuid();
                var board = new Board
                {
                    IdBoard = boardId,
                    Name = "Projeto Alpha - KaanBoard",
                    Background_Color = "#1E293B"
                };
                context.Boards.Add(board);

                // 5. Vincular o Usuário ao Quadro via sua tabela N:N (UserBoard)
                context.Set<UserBoard>().Add(new UserBoard
                {
                    IdUser = adminUser.Id,
                    IdBoard = boardId,
                    FlUserRole = "Owner"
                });

                // 6. Popular Colunas do Quadro
                var colTodoId = Guid.NewGuid();
                var colInProgressId = Guid.NewGuid();
                var colDoneId = Guid.NewGuid();

                var columns = new List<Column>
                {
                    new Column { IdColumn = colTodoId, IdBoard = boardId, TxTitle = "A Fazer", Nrposition = 1 },
                    new Column { IdColumn = colInProgressId, IdBoard = boardId, TxTitle = "Em Andamento", Nrposition = 2 },
                    new Column { IdColumn = colDoneId, IdBoard = boardId, TxTitle = "Concluído", Nrposition = 3 }
                };
                context.Set<Column>().AddRange(columns);

                // 7. Popular Tarefas (TaskItem) iniciais
                var tasks = new List<TaskItem>
                {
                    new TaskItem
                    {
                        IdTaskItem = Guid.NewGuid(),
                        IdColumn = colTodoId,
                        TxTitle = "Estruturar Banco de Dados",
                        TxDescription = "Finalizar o mapeamento das entidades no Entity Framework Core e rodar as migrations.",
                        NrPosition = 1,
                        FlCompleted = false,
                        DueDate = DateTimeOffset.UtcNow.AddDays(3),
                        UpdatedAt = DateTimeOffset.UtcNow
                    },
                    new TaskItem
                    {
                        IdTaskItem = Guid.NewGuid(),
                        IdColumn = colTodoId,
                        TxTitle = "Criar Endpoints de Autenticação",
                        TxDescription = "Implementar login com JWT e cadastro de novos usuários.",
                        NrPosition = 2,
                        FlCompleted = false,
                        DueDate = DateTimeOffset.UtcNow.AddDays(5),
                        UpdatedAt = DateTimeOffset.UtcNow
                    }
                };
                context.Set<TaskItem>().AddRange(tasks);

                await context.SaveChangesAsync();
            }
        }
    }
}