using EAgenda.Dominio.ModuloCompromisso;
using EAgenda.Infraestrutura.Arquivos.Compartilhado;

namespace EAgenda.Infraestrutura.Arquivos.ModuloCompromisso;

public class RepositorioCompromissoEmArquivo : RepositorioBaseEmArquivo<Compromisso> , IRepositorioCompromisso
{
    public RepositorioCompromissoEmArquivo(ContextoDados contexto) : base(contexto) { }

    protected override List<Compromisso> ObterRegistros()
    {
        return contexto.Compromissos;
    }
}
