using EAgenda.Dominio.ModuloTarefa;
using EAgenda.Infraestrutura.Arquivos.Compartilhado;
using EAgenda.WebApp.Extensions;
using EAgenda.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace EAgenda.WebApp.Controllers;

[Route("tarefas")]
public class TarefaController : Controller
{
    private readonly IRepositorioTarefa repositorioTarefa;

    public TarefaController(IRepositorioTarefa repositorioTarefa)
    {
        this.repositorioTarefa = repositorioTarefa;
    }

    [HttpGet]
    public IActionResult Index(string? status)
    {
        List<Tarefa> registros;

        switch (status)
        {
            case "pendentes":
                registros = repositorioTarefa.SelecionarTarefasPendentes();
                break;
            case "concluidas":
                registros = repositorioTarefa.SelecionarTarefasConcluidas();
                break;
            case "prioridades":
                registros = repositorioTarefa.SelecionarTarefasPorPrioridade();
                break;
            default:
                registros = repositorioTarefa.SelecionarTarefas();
                break;
        }

        var visualizarVM = new VisualizarTarefasViewModel(registros);

        return View(visualizarVM);
    }

    [HttpGet("cadastrar")]
    public IActionResult Cadastrar()
    {
        var cadastrarVM = new CadastrarTarefaViewModel();

        return View(cadastrarVM);
    }

    [HttpPost("cadastrar")]
    [ValidateAntiForgeryToken]
    public IActionResult Cadastrar(CadastrarTarefaViewModel cadastrarVM)
    {
        var registros = repositorioTarefa.SelecionarTarefas();

        foreach (var item in registros)
        {
            if (item.Titulo.Equals(cadastrarVM.Titulo))
            {
                ModelState.AddModelError("CadastroUnico", "Já existe uma tarefa registrada com este título.");
                break;
            }
        }

        if (!ModelState.IsValid)
            return View(cadastrarVM);

        var entidade = cadastrarVM.ParaEntidade();

        repositorioTarefa.Cadastrar(entidade);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("editar/{id:guid}")]
    public IActionResult Editar(Guid id)
    {
        var registro = repositorioTarefa.SelecionarTarefaPorId(id);

        if (registro is null)
            return RedirectToAction(nameof(Index));

        var editarVM = new EditarTarefaViewModel
        {
            Id = registro.Id,
            Titulo = registro.Titulo,
            Prioridade = registro.Prioridade,
            DataCriacao = registro.DataCriacao
        };

        return View(editarVM);
    }

    [HttpPost("editar/{id:guid}")]
    [ValidateAntiForgeryToken]
    public IActionResult Editar(Guid id, EditarTarefaViewModel editarVM)
    {
        var registros = repositorioTarefa.SelecionarTarefas();

        foreach (var item in registros)
        {
            if (!item.Id.Equals(id) && item.Titulo.Equals(editarVM.Titulo))
            {
                ModelState.AddModelError("CadastroUnico", "Já existe uma tarefa registrada com este título.");
                break;
            }
        }

        if (!ModelState.IsValid)
            return View(editarVM);

        var entidade = editarVM.ParaEntidade();

        repositorioTarefa.Editar(id, entidade);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("excluir/{id:guid}")]
    public IActionResult Excluir(Guid id)
    {
        var registro = repositorioTarefa.SelecionarTarefaPorId(id);

        if (registro is null)
            return RedirectToAction(nameof(Index));

        var excluirVM = new ExcluirTarefaViewModel
        {
            Id = registro.Id,
            Titulo = registro.Titulo
        };

        return View(excluirVM);
    }

    [HttpPost("excluir/{id:guid}")]
    public IActionResult Excluir(Guid id, ExcluirTarefaViewModel excluirVM)
    {
        repositorioTarefa.Excluir(id);
        
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("detalhes/{id:guid}")]
    public IActionResult Detalhes(Guid id)
    {
        var registro = repositorioTarefa.SelecionarTarefaPorId(id);

        var detalhesVM = new DetalhesTarefaViewModel(
            id, 
            registro.Titulo, 
            registro.Prioridade, 
            registro.DataCriacao, 
            registro.DataConclusao, 
            registro.EstaConcluida, 
            registro.PercentualConcluido, 
            registro.Itens);

        return View(detalhesVM);
    }

    [HttpPost, Route("/tarefas/{id:guid}/adicionar-item")]
    public IActionResult AdicionarTarefa(Guid id, AdicionarItemTarefaViewModel adicionarVM)
    {
        var registro = repositorioTarefa.SelecionarTarefaPorId(id);

        if (registro is null)
            return RedirectToAction(nameof(Index));

        var novoItem = new ItemTarefa(adicionarVM.Titulo, registro);

        registro.AdicionarItem(novoItem);

        repositorioTarefa.AdicionarItem(novoItem);

        var detalhesVM = new DetalhesTarefaViewModel(
            id,
            registro.Titulo,
            registro.Prioridade,
            registro.DataCriacao,
            registro.DataConclusao,
            registro.EstaConcluida,
            registro.PercentualConcluido,
            registro.Itens);

        return View("Detalhes", detalhesVM);
    }

    [HttpPost, Route("/tarefas/{id:guid}/remover-item/{idItem:guid}")]
    public IActionResult RemoverTarefa(Guid id, Guid idItem)
    {
        var registro = repositorioTarefa.SelecionarTarefaPorId(id);

        if (registro is null)
            return RedirectToAction(nameof(Index));

        var itemSelecionado = registro.ObterItem(idItem);

        if (itemSelecionado is null)
            return RedirectToAction(nameof(Index));

        registro.RemoverItem(itemSelecionado);

        repositorioTarefa.RemoverItem(itemSelecionado);


        var detalhesVM = new DetalhesTarefaViewModel(
            id,
            registro.Titulo,
            registro.Prioridade,
            registro.DataCriacao,
            registro.DataConclusao,
            registro.EstaConcluida,
            registro.PercentualConcluido,
            registro.Itens);
        
        return View("Detalhes", detalhesVM);
    }

    [HttpPost, Route("/tarefas/{id:guid}/concluir-item/{idItem:guid}")]
    public IActionResult Concluir(Guid id, Guid idItem)
    {
        var registro = repositorioTarefa.SelecionarTarefaPorId(id);

        if (registro is null)
            return RedirectToAction(nameof(Index));

        var itemSelecionado = registro.ObterItem(idItem);

        if (itemSelecionado is null)
            return RedirectToAction(nameof(Index));

        itemSelecionado.Concluir();

        repositorioTarefa.AtualizarItem(itemSelecionado);

        var detalhesVM = new DetalhesTarefaViewModel(
            id,
            registro.Titulo,
            registro.Prioridade,
            registro.DataCriacao,
            registro.DataConclusao,
            registro.EstaConcluida,
            registro.PercentualConcluido,
            registro.Itens);

        return View("Detalhes", detalhesVM);
    }

    [HttpPost, Route("/tarefas/{id:guid}/desconcluir-item/{idItem:guid}")]
    public IActionResult DesConcluir(Guid id, Guid idItem)
    {
        var registro = repositorioTarefa.SelecionarTarefaPorId(id);

        if (registro is null)
            return RedirectToAction(nameof(Index));

        var itemSelecionado = registro.ObterItem(idItem);

        if (itemSelecionado is null)
            return RedirectToAction(nameof(Index));

        itemSelecionado.MarcarPendente();

        repositorioTarefa.AtualizarItem(itemSelecionado);

        var detalhesVM = new DetalhesTarefaViewModel(
            id,
            registro.Titulo,
            registro.Prioridade,
            registro.DataCriacao,
            registro.DataConclusao,
            registro.EstaConcluida,
            registro.PercentualConcluido,
            registro.Itens);

        return View("Detalhes", detalhesVM);
    }
    
    
    [HttpPost, Route("/tarefas/{id:guid}/concluir-tarefa")]
    public IActionResult Concluir(Guid id)
    {
        var registro = repositorioTarefa.SelecionarTarefaPorId(id);

        if (registro is null)
            return RedirectToAction(nameof(Index));

        registro.Concluir();

        repositorioTarefa.Editar(id, registro);

        var detalhesVM = new DetalhesTarefaViewModel(
            id,
            registro.Titulo,
            registro.Prioridade,
            registro.DataCriacao,
            registro.DataConclusao,
            registro.EstaConcluida,
            registro.PercentualConcluido,
            registro.Itens);

        return View("Detalhes", detalhesVM);
    }

    [HttpPost, Route("/tarefas/{id:guid}/desconcluir-tarefa")]
    public IActionResult DesConcluir(Guid id)
    {
        var registro = repositorioTarefa.SelecionarTarefaPorId(id);

        if (registro is null)
            return RedirectToAction(nameof(Index));

        registro.MarcarPendente();

        repositorioTarefa.Editar(id, registro);

        var detalhesVM = new DetalhesTarefaViewModel(
            id,
            registro.Titulo,
            registro.Prioridade,
            registro.DataCriacao,
            registro.DataConclusao,
            registro.EstaConcluida,
            registro.PercentualConcluido,
            registro.Itens);

        return View("Detalhes", detalhesVM);
    }
}
