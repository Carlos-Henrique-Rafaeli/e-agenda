using EAgenda.Dominio.ModuloTarefa;
using EAgenda.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace EAgenda.Infraestrutura.Orm.ModuloTarefa;

public class RepositorioTarefaEmOrm : IRepositorioTarefa
{
    private readonly DbSet<Tarefa> tarefas;

    public RepositorioTarefaEmOrm(EAgendaDbContext contexto)
    {
        tarefas = contexto.Tarefas;
    }

    public void CadastrarRegistro(Tarefa tarefa)
    {
        tarefas.Add(tarefa);
    }

    public bool EditarRegistro(Guid idTarefa, Tarefa tarefaEditada)
    {
        var tarefaSelecionada = SelecionarRegistroPorId(idTarefa);

        if (tarefaSelecionada is null)
            return false;

        tarefaSelecionada.AtualizarRegistro(tarefaEditada);

        return true;
    }

    public bool ExcluirRegistro(Guid idTarefa)
    {
        var tarefaSelecionada = SelecionarRegistroPorId(idTarefa);

        if (tarefaSelecionada is null)
            return false;

        tarefas.Remove(tarefaSelecionada);

        return true;
    }

    public Tarefa? SelecionarRegistroPorId(Guid idTarefa)
    {
        return tarefas
            .Include(t => t.Itens)
            .FirstOrDefault(t => t.Id == idTarefa);
    }

    public List<Tarefa> SelecionarRegistros()
    {
        return tarefas.ToList();
    }

    public List<Tarefa> SelecionarTarefasConcluidas()
    {
        return tarefas
            .Where(t => t.EstaConcluida)
            .ToList();
    }

    public List<Tarefa> SelecionarTarefasPendentes()
    {
        return tarefas
            .Where(t => !t.EstaConcluida)
            .ToList();
    }

    public List<Tarefa> SelecionarTarefasPorPrioridade()
    {
        var tarefasOrdenadas = SelecionarRegistros();

        return tarefasOrdenadas.OrderByDescending(x => x.Prioridade).ToList();
    }
}
