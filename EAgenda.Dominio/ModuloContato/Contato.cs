using EAgenda.Dominio.Compartilhado;

namespace EAgenda.Dominio.ModuloContato;

public class Contato : EntidadeBase<Contato>
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public string Cargo { get; set; }
    public string Empresa { get; set; }

    public Contato()
    {
    }

    public Contato(string nome, string email, string telefone, string cargo, string empresa) : this()
    {
        Nome = nome;
        Email = email;
        Telefone = telefone;
        Cargo = cargo;
        Empresa = empresa;
    }

    public override void AtualizarRegistro(Contato registroEditado)
    {
        Nome = registroEditado.Nome;
        Email = registroEditado.Email;
        Telefone = registroEditado.Telefone;
        Cargo = registroEditado.Cargo;
        Empresa = registroEditado.Empresa;
    }
}
