using EAgenda.Dominio.ModuloCompromisso;
using EAgenda.Dominio.ModuloContato;
using EAgenda.WebApp.Models;

namespace EAgenda.WebApp.Extensions;

public static class CompromissoExtension
{
    public static Compromisso ParaEntidade(
        this FormularioCompromissoViewModel formularioVM,
        List<Contato> contatos
    )
    {
        Contato? contatoSelecionado = null;

        foreach (var c in contatos)
        {
            if (c.Id.Equals(formularioVM.ContatoId))
            {
                contatoSelecionado = c;
                break;
            }
        }

        return new Compromisso(
            formularioVM.Assunto,
            formularioVM.Data,
            formularioVM.HoraInicio,
            formularioVM.HoraTermino,
            formularioVM.Tipo,
            formularioVM.Local,
            formularioVM.Link,
            contatoSelecionado
        );
    }

    public static DetalhesCompromissoViewModel ParaDetalhesVM(this Compromisso compromisso)
    {
        return new DetalhesCompromissoViewModel(
                compromisso.Id,
                compromisso.Assunto,
                compromisso.Data,
                compromisso.HoraInicio,
                compromisso.HoraTermino,
                compromisso.Tipo,
                compromisso.Local,
                compromisso.Link,
                compromisso.Contato?.Nome
        );
    }
}
