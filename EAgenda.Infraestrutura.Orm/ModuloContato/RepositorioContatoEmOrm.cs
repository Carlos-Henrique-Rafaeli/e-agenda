using EAgenda.Dominio.ModuloContato;
using EAgenda.Infraestrutura.Orm.Compartilhado;

namespace EAgenda.Infraestrutura.Orm.ModuloContato;

public class RepositorioContatoEmOrm : IRepositorioContato
{
    private readonly EAgendaDbContext contexto;

    public RepositorioContatoEmOrm(EAgendaDbContext contexto)
    {
        this.contexto = contexto;
    }

    public void CadastrarRegistro(Contato registro)
    {
        contexto.Contatos.Add(registro);
    }

    public bool EditarRegistro(Guid idRegistro, Contato registroEditado)
    {
        var registro = SelecionarRegistroPorId(idRegistro);

        if (registro is null)
            return false;

        registro.AtualizarRegistro(registroEditado);

        return true;
    }

    public bool ExcluirRegistro(Guid idRegistro)
    {
        var registro = SelecionarRegistroPorId(idRegistro);

        if (registro is null)
            return false;

        contexto.Contatos.Remove(registro);

        return true;
    }

    public Contato? SelecionarRegistroPorId(Guid idRegistro)
    {
        return contexto.Contatos.FirstOrDefault(x => x.Id == idRegistro);
    }

    public List<Contato> SelecionarRegistros()
    {
        return contexto.Contatos.ToList();
    }
}
