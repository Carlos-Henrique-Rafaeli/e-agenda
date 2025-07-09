using EAgenda.Dominio.ModuloContato;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EAgenda.Infraestrutura.Orm.ModuloContato;

public class MapeadorContatoEmOrm : IEntityTypeConfiguration<Contato>
{
    public void Configure(EntityTypeBuilder<Contato> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.Nome)
            .IsRequired();

        builder.Property(x => x.Email)
            .IsRequired();

        builder.Property(x => x.Telefone)
            .IsRequired();

        builder.Property(x => x.Cargo)
            .IsRequired(false);

        builder.Property(x => x.Empresa)
            .IsRequired(false);
    }
}
