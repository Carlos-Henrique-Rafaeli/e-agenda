using EAgenda.Dominio.ModuloCategoria;
using EAgenda.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

namespace EAgenda.Infraestrutura.Orm.ModuloCategoria;

public class RepositorioCategoriaEmOrm : RepositorioBaseEmOrm<Categoria>, IRepositorioCategoria
{
    public RepositorioCategoriaEmOrm(EAgendaDbContext contexto) : base(contexto) { }

    public override Categoria? SelecionarRegistroPorId(Guid idRegistro)
    {
        return registros
            .Include(c => c.Despesas)
            .FirstOrDefault(x => x.Id.Equals(idRegistro));
    }
}
