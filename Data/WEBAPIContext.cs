using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WEBAPI.Models;

namespace WEBAPI.Data
{
    public class WEBAPIContext : DbContext
    {
        public WEBAPIContext (DbContextOptions<WEBAPIContext> options)
            : base(options)
        {
        }

        public DbSet<WEBAPI.Models.Tarefa> Tarefa { get; set; } = default!;
    }
}
