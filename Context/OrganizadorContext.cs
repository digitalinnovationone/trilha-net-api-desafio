using Microsoft.EntityFrameworkCore;
using TrilhaApiDesafio.Map;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Context
{
    public class OrganizadorContext : DbContext
    {
        public OrganizadorContext(DbContextOptions<OrganizadorContext> options) : base(options)
        {
            
        }

        public DbSet<Tarefa> Tarefas { get; set; }

        public DbSet<Teste> Teste { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TarefaMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}