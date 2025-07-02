using EAgenda.Dominio.ModuloTarefa;
using EAgenda.WebApp.Models;

namespace EAgenda.WebApp.Extensions;

public static class TarefaExtensions
{
    public static Tarefa ParaEntidade(this FormularioTarefaViewModel formularioVM)
    {
        return new Tarefa(formularioVM.Titulo, formularioVM.Prioridade, formularioVM.DataCriacao);
    }

    public static DetalhesTarefaViewModel ParaDetalhesVM(this Tarefa tarefa)
    {
        return new DetalhesTarefaViewModel
        (
            tarefa.Id,
            tarefa.Titulo,
            tarefa.Prioridade,
            tarefa.DataCriacao,
            tarefa.DataConclusao,
            tarefa.EstaConcluida,
            tarefa.PercentualConcluido,
            tarefa.Itens
        );
    }
}
