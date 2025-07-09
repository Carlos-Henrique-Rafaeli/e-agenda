using EAgenda.Dominio.ModuloContato;
using EAgenda.Dominio.ModuloCompromisso;
using EAgenda.Infraestrutura.Arquivos.Compartilhado;
using EAgenda.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using EAgenda.Infraestrutura.Orm.Compartilhado;

namespace EAgenda.WebApp.Controllers;

[Route("contatos")]
public class ContatoController : Controller
{
    private readonly EAgendaDbContext contexto;
    private readonly IRepositorioContato repositorioContato;

    public ContatoController(EAgendaDbContext contexto, IRepositorioContato repositorioContato)
    {
        this.contexto = contexto;
        this.repositorioContato = repositorioContato;
    }

    public IActionResult Index()
    {
        var contatos = repositorioContato.SelecionarRegistros();
        var vm = new VisualizarContatosViewModel(contatos);
        return View(vm);
    }

    [HttpGet("cadastrar")]
    public IActionResult Cadastrar()
    {
        return View(new CadastrarContatoViewModel());
    }

    [HttpPost("cadastrar")]
    public IActionResult Cadastrar(CadastrarContatoViewModel vm)
    {
        var contatosExistentes = repositorioContato.SelecionarRegistros();

        if (contatosExistentes.Any(c => c.Email.Equals(vm.Email, StringComparison.OrdinalIgnoreCase)))
        {
            ModelState.AddModelError("Email", "Já existe um contato cadastrado com esse email.");
            return View(vm);
        }

        if (contatosExistentes.Any(c => c.Telefone == vm.Telefone))
        {
            ModelState.AddModelError("Telefone", "Já existe um contato cadastrado com esse telefone.");
            return View(vm);
        }

        if (!ModelState.IsValid)
            return View(vm);

        var transacao = contexto.Database.BeginTransaction();

        try
        {
            repositorioContato.CadastrarRegistro(vm.ParaEntidade());

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
    public IActionResult Editar(Guid id)
    {
        var contato = repositorioContato.SelecionarRegistroPorId(id);

        if (contato == null)
            return NotFound();

        var vm = new EditarContatoViewModel(contato.Id, contato.Nome, contato.Email, contato.Telefone, contato.Cargo, contato.Empresa);

        return View(vm);
    }

    [HttpPost("editar/{id:guid}")]
    public IActionResult Editar(Guid id, EditarContatoViewModel vm)
    {
        var contatosExistentes = repositorioContato.SelecionarRegistros().Where(c => c.Id != id);

        if (contatosExistentes.Any(c => c.Email.Equals(vm.Email, StringComparison.OrdinalIgnoreCase)))
        {
            ModelState.AddModelError("Email", "Já existe um contato cadastrado com esse email.");
            return View(vm);
        }

        if (contatosExistentes.Any(c => c.Telefone == vm.Telefone))
        {
            ModelState.AddModelError("Telefone", "Já existe um contato cadastrado com esse telefone.");
            return View(vm);
        }

        if (!ModelState.IsValid)
            return View(vm);

        var transacao = contexto.Database.BeginTransaction();

        try
        {
            repositorioContato.EditarRegistro(id, vm.ParaEntidade());

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
    public IActionResult Excluir(Guid id)
    {
        var registroSelecionado = repositorioContato.SelecionarRegistroPorId(id);

        var excluirVM = new ExcluirContatoViewModel(
            registroSelecionado.Id,
            registroSelecionado.Nome
            );

        return View(excluirVM);
    }

    [HttpPost("excluir/{id:guid}")]
    [ValidateAntiForgeryToken]
    public IActionResult ExcluirConfirmado(Guid id)
    {
        var transacao = contexto.Database.BeginTransaction();

        try
        {
            repositorioContato.ExcluirRegistro(id);

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
}
