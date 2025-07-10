using EAgenda.Dominio.ModuloDespesa;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EAgenda.Infraestrutura.Orm.ModuloDespesa;

public class MapeadorDespesaEmOrm : IEntityTypeConfiguration<Despesa>
{
    public void Configure(EntityTypeBuilder<Despesa> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .IsRequired();
        
        builder.Property(x => x.Descricao)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Valor)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.DataOcorencia)
            .IsRequired();

        builder.Property(x => x.FormaPagamento)
            .IsRequired();

        builder.HasMany(x => x.Categorias)
            .WithMany(x => x.Despesas)
            .UsingEntity(j => j.ToTable("DespesaCategoria"));
    }
}
