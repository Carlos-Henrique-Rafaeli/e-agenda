using EAgenda.Dominio.ModuloContato;
using EAgenda.Infraestrutura.Orm.ModuloContato;
using Microsoft.EntityFrameworkCore;

namespace EAgenda.Infraestrutura.Orm.Compartilhado;

public class EAgendaDbContext : DbContext
{
    public DbSet<Contato> Contatos { get; set; }

    public EAgendaDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MapeadorContatoEmOrm());

        base.OnModelCreating(modelBuilder);
    }
}
