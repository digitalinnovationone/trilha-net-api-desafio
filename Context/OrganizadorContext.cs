using Microsoft.EntityFrameworkCore;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Tarefa> Tarefas { get; set; }

        // Construtor necessário para o AddDbContext funcionar
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tarefa>()
                .Property(t => t.Titulo)
                .HasColumnType("TEXT");  // MySQL-compatible type for large text
            modelBuilder.Entity<Tarefa>()
                .Property(t => t.Descricao)
                .HasColumnType("TEXT");  // MySQL-compatible type for large text
            modelBuilder.Entity<Tarefa>()
                .Property(t => t.Data)
                .HasColumnType("DATETIME");  // MySQL-compatible type for date and time
        }
    }
}
