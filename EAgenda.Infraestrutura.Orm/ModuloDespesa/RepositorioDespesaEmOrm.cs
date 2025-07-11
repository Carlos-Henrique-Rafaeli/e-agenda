using EAgenda.Dominio.ModuloDespesa;
using EAgenda.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace EAgenda.Infraestrutura.Orm.ModuloDespesa;

public class RepositorioDespesaEmOrm : RepositorioBaseEmOrm<Despesa>, IRepositorioDespesa
{
    public RepositorioDespesaEmOrm(EAgendaDbContext contexto) : base(contexto) { }

    public override Despesa? SelecionarRegistroPorId(Guid idRegistro)
    {
        return registros
            .Include(c => c.Categorias)
            .FirstOrDefault(x => x.Id.Equals(idRegistro));
    }
}
