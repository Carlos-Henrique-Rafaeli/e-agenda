using EAgenda.Dominio.ModuloCategoria;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EAgenda.Infraestrutura.Orm.ModuloCategoria;

public class MapeadorCategoriaEmOrm : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.Titulo)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasMany(d => d.Despesas)
            .WithMany(c => c.Categorias)
            .UsingEntity(j => j.ToTable("CategoriaDespesa"));
    }
}
