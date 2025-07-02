using EAgenda.Dominio.Compartilhado;

namespace EAgenda.Dominio.ModuloTarefa;

public class Tarefa : EntidadeBase<Tarefa>
{
    public string Titulo { get; set; }
    public Prioridade Prioridade { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime? DataConclusao { get; set; }
    public bool EstaConcluida { get; set; }
    public List<ItemTarefa> Itens { get; set; } = new List<ItemTarefa>();
    public decimal PercentualConcluido { get 
        {
            if (Itens.Count == 0)
                return default;

            int qtdConcluidos = Itens.Count(i => i.EstaConcluida);

            decimal percentualBase = qtdConcluidos / (decimal)Itens.Count * 100;

            return Math.Round(percentualBase, 2);
        } 
    }
    
    public Tarefa() { }

    public Tarefa(
        string titulo, 
        Prioridade prioridade, 
        DateTime dataCriacao) : this()
    {
        Titulo = titulo;
        Prioridade = prioridade;
        DataCriacao = dataCriacao;
        EstaConcluida = false;
    }

    public void Concluir()
    {
        EstaConcluida = true;
        DataConclusao = DateTime.Now;
    }

    public void MarcarPendente()
    {
        EstaConcluida = false;
        DataConclusao = null;
    }

    public ItemTarefa? ObterItem(Guid idItem)
    {
        return Itens.Find(i => i.Id.Equals(idItem));
    }

    public ItemTarefa AdicionarItem(string titulo)
    {
        var item = new ItemTarefa(titulo, this);

        Itens.Add(item);

        MarcarPendente();

        return item;
    }

    public ItemTarefa AdicionarItem(ItemTarefa item)
    {
        Itens.Add(item);

        return item;
    }

    public bool RemoverItem(ItemTarefa item)
    {
        Itens.Remove(item);

        MarcarPendente();

        return true;
    }

    public void ConcluirItem(ItemTarefa item)
    {
        item.Concluir();
    }

    public void MarcarItemPendente(ItemTarefa item)
    {
        item.MarcarPendente();

        MarcarPendente();
    }

    public override void AtualizarRegistro(Tarefa registroEditado)
    {
        Titulo = registroEditado.Titulo;
        Prioridade = registroEditado.Prioridade;
        DataCriacao = registroEditado.DataCriacao;
    }
}
