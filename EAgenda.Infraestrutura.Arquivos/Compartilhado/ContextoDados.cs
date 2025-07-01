using System.Text.Json.Serialization;
using System.Text.Json;
using EAgenda.Dominio.ModuloContato;
using EAgenda.Dominio.ModuloTarefa;
using EAgenda.Dominio.ModuloCategoria;
using EAgenda.Dominio.ModuloCompromisso;
using EAgenda.Dominio.ModuloDespesa;

namespace EAgenda.Infraestrutura.Arquivos.Compartilhado;

public class ContextoDados
{
    private string pastaRaiz = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "AcademiaProgramador2025");
    private string arquivoArmazenamento = "dados.json";
    private string pastaPrincipal = "EAgenda";

    public List<Tarefa> Tarefas { get; set; }
    public List<Contato> Contatos { get; set; }
    public List<Compromisso> Compromissos { get; set; }
    public List<Categoria> Categorias { get; set; }
    public List<Despesa> Despesas { get; set; }

    public ContextoDados()
    {
        Tarefas = new List<Tarefa>();
        Categorias = new List<Categoria>();
        Despesas = new List<Despesa>();
        Contatos = new List<Contato>();
        Compromissos = new List<Compromisso>();
    }

    public ContextoDados(bool carregarDados) : this()
    {
        if (carregarDados)
            Carregar();
    }

    public void Salvar()
    {
        if (!Directory.Exists(pastaRaiz))
            Directory.CreateDirectory(pastaRaiz);

        string pastaProjeto = Path.Combine(pastaRaiz, pastaPrincipal);

        if (!Directory.Exists(pastaProjeto))
            Directory.CreateDirectory(pastaProjeto);

        JsonSerializerOptions jsonOptions = new JsonSerializerOptions();
        jsonOptions.WriteIndented = true;
        jsonOptions.ReferenceHandler = ReferenceHandler.Preserve;

        string caminhoCompleto = Path.Combine(pastaProjeto, arquivoArmazenamento);

        string json = JsonSerializer.Serialize(this, jsonOptions);

        File.WriteAllText(caminhoCompleto, json);
    }

    public void Carregar()
    {
        string pastaProjeto = Path.Combine(pastaRaiz, pastaPrincipal);

        string caminhoCompleto = Path.Combine(pastaProjeto, arquivoArmazenamento);

        if (!File.Exists(caminhoCompleto)) return;

        string json = File.ReadAllText(caminhoCompleto);

        if (string.IsNullOrWhiteSpace(json)) return;

        JsonSerializerOptions jsonOptions = new JsonSerializerOptions();
        jsonOptions.ReferenceHandler = ReferenceHandler.Preserve;

        ContextoDados contextoArmazenado = JsonSerializer.Deserialize<ContextoDados>(
            json,
            jsonOptions
        )!;

        if (contextoArmazenado == null) return;

        Tarefas = contextoArmazenado.Tarefas;
        Contatos = contextoArmazenado.Contatos;
        Categorias = contextoArmazenado.Categorias;
        Despesas = contextoArmazenado.Despesas;
        Compromissos = contextoArmazenado.Compromissos;
    }
}
