using EAgenda.Dominio.ModuloContato;
using EAgenda.Dominio.ModuloCompromisso;
using EAgenda.Infraestrutura.Arquivos.Compartilhado;
using EAgenda.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace EAgenda.WebApp.Controllers;

[Route("contatos")]
public class ContatoController : Controller
{
    private readonly IRepositorioContato repositorioContato;
    private readonly IRepositorioCompromisso repositorioCompromisso;

    public ContatoController(
        IRepositorioContato repositorioContato, 
        IRepositorioCompromisso repositorioCompromisso)
    {
        this.repositorioContato = repositorioContato;
        this.repositorioCompromisso = repositorioCompromisso;
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
        if (!ModelState.IsValid)
            return View(vm);

        // Verifica duplicidade de email ou telefone
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

        repositorioContato.CadastrarRegistro(vm.ParaEntidade());

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
        if (!ModelState.IsValid)
            return View(vm);

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

        repositorioContato.EditarRegistro(id, vm.ParaEntidade());

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
        repositorioContato.ExcluirRegistro(id);

        return RedirectToAction(nameof(Index));
    }

}
