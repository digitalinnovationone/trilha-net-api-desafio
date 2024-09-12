using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrilhaApiDesafio.Models
{
    public class Tarefa
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar(255)")] // Configurar Titulo como varchar(255)
        public string Titulo { get; set; }

        [Column(TypeName = "text")] // Configurar Descricao como TEXT
        public string Descricao { get; set; }

        public DateTime Data { get; set; }

        public EnumStatusTarefa Status { get; set; }
    }

}