using EAgenda.Dominio.ModuloDespesa;
using EAgenda.Infraestrutura.Orm.Compartilhado;

namespace EAgenda.Infraestrutura.Orm.ModuloDespesa;

public class RepositorioDespesaEmOrm : RepositorioBaseEmOrm<Despesa>, IRepositorioDespesa
{
    public RepositorioDespesaEmOrm(EAgendaDbContext contexto) : base(contexto) { }
}
