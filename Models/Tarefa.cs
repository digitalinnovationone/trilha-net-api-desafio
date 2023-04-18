using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrilhaApiDesafio.Models
{    
    public class Tarefa
    {   
        public Tarefa()
        {
            Id = Id;
        }

        public Tarefa(string titulo, string descricao, DateTime data, EnumStatusTarefa status)
        : this()
        {
            Titulo = titulo;
            Descricao = descricao;
            Data = data;
            Status = status;
        }

        public int Id { get; set; }        
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }    
        public EnumStatusTarefa Status { get; set; }    
        
    }
}