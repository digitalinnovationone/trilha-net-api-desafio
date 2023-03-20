using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Context.Mapping
{
    public class TarefaMapping : IEntityTypeConfiguration<Tarefa>
    {
        public void Configure(EntityTypeBuilder<Tarefa> builder)
        {
            builder.ToTable("Tarefa");

            builder.HasKey(x=> x.Id);

            builder.Property(x=> x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

            builder.Property(x=> x.Titulo)
            .IsRequired()
            .HasColumnName("Titulo")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(20);

            builder.Property(x=> x.Descricao)
            .IsRequired()
            .HasColumnName("Descricao")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(30);

            builder.Property(x=> x.Data)
            .IsRequired()
            .HasColumnName("DataCriacao")
            .HasColumnType("SMALLDATETIME")
            .HasMaxLength(60);            

            builder.Property(x=> x.Status)
            .IsRequired()
            .HasColumnName("Status")
            .HasConversion<string>()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(20);

            builder.HasIndex(x=> x.Titulo,"IX_Tarefa_Name")            
            .IsUnique();
        }
    }
}