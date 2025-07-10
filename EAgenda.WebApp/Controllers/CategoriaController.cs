using EAgenda.Dominio.ModuloCategoria;
using EAgenda.Dominio.ModuloDespesa;
using EAgenda.Infraestrutura.Arquivos.Compartilhado;
using EAgenda.Infraestrutura.Orm.Compartilhado;
using EAgenda.WebApp.Extensions;
using EAgenda.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace EAgenda.WebApp.Controllers;

[Route("categorias")]
public class CategoriaController : Controller
{
    private readonly EAgendaDbContext contexto;
    private readonly IRepositorioCategoria repositorioCategoria;

    public CategoriaController(EAgendaDbContext contexto,
        IRepositorioCategoria repositorioCategoria)
    {
        this.contexto = contexto;
        this.repositorioCategoria = repositorioCategoria;
    }

    public IActionResult Index()
    {
        var registros = repositorioCategoria.SelecionarRegistros();

        var visualizarVM = new VisualizarCategoriasViewModel(registros);

        return View(visualizarVM);
    }

    [HttpGet("cadastrar")]
    public IActionResult Cadastrar()
    {
        var cadastrarVM = new CadastrarCategoriaViewModel();

        return View(cadastrarVM);
    }

    [HttpPost("cadastrar")]
    [ValidateAntiForgeryToken]
    public ActionResult Cadastrar(CadastrarCategoriaViewModel cadastrarVM)
    {
        var registros = repositorioCategoria.SelecionarRegistros();

        foreach (var item in registros)
        {
            if (item.Titulo.Equals(cadastrarVM.Titulo))
            {
                ModelState.AddModelError("CadastroUnico", "Já existe um garçom registrado com este nome.");
                break;
            }

            if (item.Titulo.Equals(cadastrarVM.Titulo))
            {
                ModelState.AddModelError("CadastroUnico", "Já existe um garçom registrado com este CPF.");
                break;
            }
        }

        if (!ModelState.IsValid)
            return View(cadastrarVM);

        var transacao = contexto.Database.BeginTransaction();

        try
        {
            var entidade = cadastrarVM.ParaEntidade();

            repositorioCategoria.CadastrarRegistro(entidade);
            
            contexto.SaveChanges();

            transacao.Commit();
        }
        catch (Exception)
        {
            transacao.Rollback();

            throw;
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("editar/{id:guid}")]
    public ActionResult Editar(Guid id)
    {
        var registroSelecionado = repositorioCategoria.SelecionarRegistroPorId(id);

        var editarVM = new EditarCategoriaViewModel(
            id,
            registroSelecionado.Titulo
        );

        return View(editarVM);
    }

    [HttpPost("editar/{id:guid}")]
    [ValidateAntiForgeryToken]
    public ActionResult Editar(Guid id, EditarCategoriaViewModel editarVM)
    {
        var registros = repositorioCategoria.SelecionarRegistros();

        foreach (var item in registros)
        {
            if (!item.Id.Equals(id) && item.Titulo.Equals(editarVM.Titulo))
            {
                ModelState.AddModelError("CadastroUnico", "Já existe uma categoria registrada com este título.");
                break;
            }
        }

        if (!ModelState.IsValid)
            return View(editarVM);

        var transacao = contexto.Database.BeginTransaction();

        try
        {
            var entidadeEditada = editarVM.ParaEntidade();

            repositorioCategoria.EditarRegistro(id, entidadeEditada);

            contexto.SaveChanges();

            transacao.Commit();
        }
        catch (Exception)
        {
            transacao.Rollback();

            throw;
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("excluir/{id:guid}")]
    public ActionResult Excluir(Guid id)
    {
        var registroSelecionado = repositorioCategoria.SelecionarRegistroPorId(id);

        var excluirVM = new ExcluirCategoriaViewModel(registroSelecionado.Id, registroSelecionado.Titulo);

        return View(excluirVM);
    }

    [HttpPost("excluir/{id:guid}")]
    [ValidateAntiForgeryToken]
    public ActionResult ExcluirConfirmado(Guid id)
    {
        var categoria = repositorioCategoria.SelecionarRegistroPorId(id);

        if (categoria == null)
            return NotFound();

        if (categoria.Despesas != null && categoria.Despesas.Any())
        {
            ModelState.AddModelError("ExclusaoNaoPermitida", "Não é possível excluir uma categoria que possui despesas vinculadas.");

            var visualizarVM = new VisualizarCategoriasViewModel(repositorioCategoria.SelecionarRegistros());
            return View("Index", visualizarVM);
        }

        var transacao = contexto.Database.BeginTransaction();

        try
        {
            repositorioCategoria.ExcluirRegistro(id);

            contexto.SaveChanges();

            transacao.Commit();
        }
        catch (Exception)
        {
            transacao.Rollback();

            throw;
        }


        return RedirectToAction(nameof(Index));
    }


    [HttpGet("detalhes/{id:guid}")]
    public ActionResult Detalhes(Guid id)
    {
        var registroSelecionado = repositorioCategoria.SelecionarRegistroPorId(id);

        var detalhesVM = new DetalhesCategoriaViewModel(
            id,
            registroSelecionado.Titulo,
            registroSelecionado.Despesas?.Count ?? 0,
            registroSelecionado.Despesas?.Select(d => d.Descricao).ToList() ?? new()
        );

        return View(detalhesVM);
    }
}
