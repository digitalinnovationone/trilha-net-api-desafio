using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WEBAPI.Models;

namespace WEBAPI.Data
{
    public class OrganizadorContext : DbContext
    {
        public OrganizadorContext(DbContextOptions<OrganizadorContext> options) : base(options)
        {
        }

        public DbSet<Tarefa> Tarefas { get; set; }
    }
}
