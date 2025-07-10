using EAgenda.Dominio.ModuloContato;
using EAgenda.Infraestrutura.Orm.Compartilhado;

namespace EAgenda.Infraestrutura.Orm.ModuloContato;

public class RepositorioContatoEmOrm : RepositorioBaseEmOrm<Contato>, IRepositorioContato
{
    public RepositorioContatoEmOrm(EAgendaDbContext contexto) : base(contexto) { }
}
