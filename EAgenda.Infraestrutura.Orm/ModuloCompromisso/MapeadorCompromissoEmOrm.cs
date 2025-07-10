using EAgenda.Dominio.ModuloCompromisso;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EAgenda.Infraestrutura.Orm.ModuloCompromisso;

public class MapeadorCompromissoEmOrm : IEntityTypeConfiguration<Compromisso>
{
    public void Configure(EntityTypeBuilder<Compromisso> builder)
    {
        builder.Property(x => x.Assunto)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Data)
            .IsRequired();

        builder.Property(x => x.HoraInicio)
            .IsRequired();

        builder.Property(x => x.HoraTermino)
            .IsRequired();

        builder.Property(x => x.Tipo)
            .IsRequired();

        builder.Property(x => x.Local)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(x => x.Link)
            .HasMaxLength(200)
            .IsRequired(false);

        builder.HasOne(x => x.Contato)
            .WithMany()
            .IsRequired(false);
    }
}
