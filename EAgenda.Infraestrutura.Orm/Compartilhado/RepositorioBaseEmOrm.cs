using EAgenda.Dominio.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace EAgenda.Infraestrutura.Orm.Compartilhado;

public abstract class RepositorioBaseEmOrm<T> where T : EntidadeBase<T>
{
    private readonly DbSet<T> registros;

    public RepositorioBaseEmOrm(EAgendaDbContext contexto)
    {
        this.registros = contexto.Set<T>();
    }

    public void CadastrarRegistro(T novoRegistro)
    {
        registros.Add(novoRegistro);
    }

    public bool EditarRegistro(Guid idRegistro, T registroEditado)
    {
        var registroSelecionado = SelecionarRegistroPorId(idRegistro);

        if (registroSelecionado is null)
            return false;

        registroSelecionado.AtualizarRegistro(registroEditado);

        return true;
    }

    public bool ExcluirRegistro(Guid idRegistro)
    {
        var registroSelecionado = SelecionarRegistroPorId(idRegistro);

        if (registroSelecionado is null)
            return false;

        registros.Remove(registroSelecionado);

        return true;
    }

    public virtual T? SelecionarRegistroPorId(Guid idRegistro)
    {
        return registros.FirstOrDefault(x => x.Id.Equals(idRegistro));
    }

    public virtual List<T> SelecionarRegistros()
    {
        return registros.ToList();
    }
}
