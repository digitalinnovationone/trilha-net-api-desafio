using Microsoft.EntityFrameworkCore;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.TarefasContextContext;

    public class TarefasContextContexts : DbContext
    {
        public TarefasContextContexts(DbContextOptions<TarefasContextContexts> options) : base(options)
        {
            
        }

        public DbSet<Tarefa> Tarefas => Set<Tarefa>();
    }
