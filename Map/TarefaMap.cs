using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Map
{
    public class TarefaMap : IEntityTypeConfiguration<Tarefa>
    {
        public void Configure(EntityTypeBuilder<Tarefa> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Titulo).HasMaxLength(155);
            builder.Property(x => x.Descricao).HasMaxLength(500);
            builder.Property(x => x.Data);
            builder.Property(x => x.Status).HasMaxLength(100);
        }
    }
}
