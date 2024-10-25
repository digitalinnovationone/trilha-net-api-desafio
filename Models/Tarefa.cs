using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TrilhaApiDesafio.Models
{
    public class Tarefa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
         
        [StringLength(255), NotNull]
        public string Titulo { get; set; }

        public string Descricao { get; set; }
        
        public DateTime Data { get; set; }

        public EnumStatusTarefa Status { get; set; }
    }
}