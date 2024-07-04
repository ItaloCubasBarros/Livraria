using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using app.DTO.UserDTO;
using app.DTO; // Importe a entidade ApplicationUser

namespace app.Data
{
    public class AppDbContext : DbContext
    {
    


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<UserDTO> Users { get; set; }
        public DbSet<LivroDTO> Livros { get; set; }
        public DbSet<EmprestimoDTO> Emprestimos { get; set; }
        public DbSet<AlunoDTO> Alunos { get; set; }

    }
}
