using EAgenda.Dominio.ModuloTarefa;
using EAgenda.Infraestrutura.Arquivos.Compartilhado;

namespace EAgenda.Infraestrutura.Arquivos.ModuloTarefa;

public class RepositorioTarefaEmArquivo : RepositorioBaseEmArquivo<Tarefa>
{
    public RepositorioTarefaEmArquivo(ContextoDados contexto) : base(contexto) { }

    public List<Tarefa> SelecionarTarefasConcluidas()
    {
        return registros.FindAll(tarefa => tarefa.EstaConcluida);
    }

    public List<Tarefa> SelecionarTarefasPendentes()
    {
        return registros.FindAll(tarefa => !tarefa.EstaConcluida);
    }

    public List<Tarefa> SelecionarTarefasPorPrioridade()
    {
        return registros.OrderByDescending((x) => x.Prioridade).ToList();
    }

    protected override List<Tarefa> ObterRegistros()
    {
        return contexto.Tarefas;
    }
}
