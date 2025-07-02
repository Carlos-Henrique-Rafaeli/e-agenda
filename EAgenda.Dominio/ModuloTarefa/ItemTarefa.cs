using EAgenda.Dominio.Compartilhado;

namespace EAgenda.Dominio.ModuloTarefa;

public class ItemTarefa : EntidadeBase<ItemTarefa>
{
    public string Titulo { get; set; }
    public bool EstaConcluida { get; set; }
    public Tarefa Tarefa { get; set; }

    public ItemTarefa() { }

    public ItemTarefa(string titulo, Tarefa tarefa) : this()
    {
        Titulo = titulo;
        Tarefa = tarefa;
        EstaConcluida = false;
    }

    public void Concluir()
    {
        EstaConcluida = true;
    }

    public void MarcarPendente()
    {
        EstaConcluida = false;
    }

    public override void AtualizarRegistro(ItemTarefa registroEditado)
    {
        throw new NotImplementedException();
    }
}
