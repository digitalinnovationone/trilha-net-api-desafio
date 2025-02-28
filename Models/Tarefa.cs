using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrilhaApiDesafio.Models
{
    public class Tarefa
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = default!;
        public string Descricao { get; set; } = default!;
        public DateTime Data { get; set; }
        public EnumStatusTarefa Status { get; set; }
    }
}