using EAgenda.Dominio.ModuloCategoria;
using EAgenda.Dominio.ModuloCompromisso;
using EAgenda.Dominio.ModuloContato;
using EAgenda.Dominio.ModuloDespesa;
using Microsoft.EntityFrameworkCore;

namespace EAgenda.Infraestrutura.Orm.Compartilhado;

public class EAgendaDbContext : DbContext
{
    public DbSet<Contato> Contatos { get; set; }
    public DbSet<Compromisso> Compromissos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Despesa> Despesas { get; set; }

    public EAgendaDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var assembly = typeof(EAgendaDbContext).Assembly;

        modelBuilder.ApplyConfigurationsFromAssembly(assembly);

        base.OnModelCreating(modelBuilder);
    }
}
