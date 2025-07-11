using EAgenda.Dominio.ModuloTarefa;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EAgenda.Infraestrutura.Orm.ModuloTarefa;

internal class MapeadorTarefaEmOrm : IEntityTypeConfiguration<Tarefa>
{
    public void Configure(EntityTypeBuilder<Tarefa> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.Titulo)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Prioridade)
            .IsRequired();

        builder.Property(x => x.DataCriacao)
            .IsRequired();

        builder.Property(x => x.DataConclusao)
            .IsRequired(false);

        builder.Property(x => x.EstaConcluida)
            .IsRequired();

        builder.HasMany(x => x.Itens)
            .WithOne(x => x.Tarefa);
    }
}
