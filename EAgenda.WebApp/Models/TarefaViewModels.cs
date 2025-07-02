using EAgenda.Dominio.ModuloTarefa;
using EAgenda.WebApp.Extensions;
using System.ComponentModel.DataAnnotations;

namespace EAgenda.WebApp.Models;

public abstract class FormularioTarefaViewModel
{
    [Required(ErrorMessage = "O campo \"Título\" é obrigatório.")]
    [MinLength(2, ErrorMessage = "O campo \"Título\" precisa conter ao menos 2 caracteres.")]
    [MaxLength(100, ErrorMessage = "O campo \"Título\" precisa conter no máximo 100 caracteres.")]
    public string Titulo { get; set; }
    [Required(ErrorMessage = "O campo \"Prioridade\" é obrigatório.")]
    public Prioridade Prioridade { get; set; }
    [Required(ErrorMessage = "O campo \"Data de Criação\" é obrigatório.")]
    public DateTime DataCriacao { get; set; }
}

public class CadastrarTarefaViewModel : FormularioTarefaViewModel
{
    public CadastrarTarefaViewModel() { }

    public CadastrarTarefaViewModel(
        string titulo, 
        Prioridade prioridade, 
        DateTime dataCriacao) : this()
    {
        Titulo = titulo;
        Prioridade = prioridade;
        DataCriacao = dataCriacao;
    }
}

public class EditarTarefaViewModel : FormularioTarefaViewModel
{
    public Guid Id { get; set; }

    public EditarTarefaViewModel() { }

    public EditarTarefaViewModel(
        Guid id, 
        string titulo,
        Prioridade prioridade,
        DateTime dataCriacao) : this()
    {
        Id = id;
        Titulo = titulo;
        Prioridade = prioridade;
        DataCriacao = dataCriacao;
    }
}

public class ExcluirTarefaViewModel
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }

    public ExcluirTarefaViewModel() { }

    public ExcluirTarefaViewModel(Guid id, string titulo) : this()
    {
        Id = id;
        Titulo = titulo;
    }
}

public class VisualizarTarefasViewModel
{
    public List<DetalhesTarefaViewModel> Registros { get; }

    public VisualizarTarefasViewModel(List<Tarefa> tarefas)
    {
        Registros = [];
        foreach (var i in tarefas)
        {
            var detalhesVM = i.ParaDetalhesVM();

            Registros.Add(detalhesVM);
        }
    }
}

public class DetalhesTarefaViewModel
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }
    public Prioridade Prioridade { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime? DataConclusao { get; set; }
    public bool EstaConcluida { get; set; }
    public decimal PercentualConcluido { get; set; }
    public List<ItemTarefaViewModel> ItensTarefas { get; set; }

    public DetalhesTarefaViewModel(
        Guid id, 
        string titulo, 
        Prioridade prioridade, 
        DateTime dataCriacao, 
        DateTime? dataConclusao, 
        bool estaConcluida, 
        decimal percentualConcluido, 
        List<ItemTarefa> itensTarefas)
    {
        Id = id;
        Titulo = titulo;
        Prioridade = prioridade;
        DataCriacao = dataCriacao;
        DataConclusao = dataConclusao;
        EstaConcluida = estaConcluida;
        PercentualConcluido = percentualConcluido;
        ItensTarefas = new List<ItemTarefaViewModel>();

        if (itensTarefas == null) return;

        foreach (var item in itensTarefas)
        {
            var itemVM = new ItemTarefaViewModel(
                item.Id,
                item.Titulo,
                item.EstaConcluida,
                item.Tarefa);

            ItensTarefas.Add(itemVM);
        }
    }
}

public class ItemTarefaViewModel
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }
    public bool EstaConcluida { get; set; }
    public Tarefa Tarefa { get; set; }
    public ItemTarefaViewModel(Guid id, string titulo, bool estaConcluida, Tarefa tarefa)
    {
        Id = id;
        Titulo = titulo;
        EstaConcluida = estaConcluida;
        Tarefa = tarefa;
    }
}

public class AdicionarItemTarefaViewModel
{
    public Guid Id { get; set; }
    [Required(ErrorMessage = "O campo \"Título\" é obrigatório.")]
    [MinLength(2, ErrorMessage = "O campo \"Título\" precisa conter ao menos 2 caracteres.")]
    public string Titulo { get; set; }
}