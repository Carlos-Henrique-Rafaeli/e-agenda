using EAgenda.Dominio.ModuloCategoria;
using EAgenda.Infraestrutura.Orm.Compartilhado;

namespace EAgenda.Infraestrutura.Orm.ModuloCategoria;

public class RepositorioCategoriaEmOrm : RepositorioBaseEmOrm<Categoria>, IRepositorioCategoria
{
    public RepositorioCategoriaEmOrm(EAgendaDbContext contexto) : base(contexto) { }
}
