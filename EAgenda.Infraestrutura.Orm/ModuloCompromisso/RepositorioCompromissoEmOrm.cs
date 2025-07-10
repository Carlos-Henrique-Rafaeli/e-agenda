using EAgenda.Dominio.ModuloCompromisso;
using EAgenda.Infraestrutura.Orm.Compartilhado;

namespace EAgenda.Infraestrutura.Orm.ModuloCompromisso;

public class RepositorioCompromissoEmOrm : RepositorioBaseEmOrm<Compromisso>, IRepositorioCompromisso
{
    public RepositorioCompromissoEmOrm(EAgendaDbContext contexto) : base(contexto) { }
}
