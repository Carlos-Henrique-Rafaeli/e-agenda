using EAgenda.Dominio.ModuloTarefa;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EAgenda.Infraestrutura.Orm.ModuloTarefa;

public class MapeadorItemTarefaEmOrm : IEntityTypeConfiguration<ItemTarefa>
{
    public void Configure(EntityTypeBuilder<ItemTarefa> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.Titulo)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.EstaConcluida)
            .IsRequired();

        builder.HasOne(x => x.Tarefa)
            .WithMany(x => x.Itens)
            .IsRequired();
    }
}
