using EAgenda.Dominio.ModuloCompromisso;
using EAgenda.Dominio.ModuloCategoria;
using EAgenda.Dominio.ModuloContato;
using EAgenda.Dominio.ModuloDespesa;
using EAgenda.Dominio.ModuloTarefa;
using EAgenda.Infraestrutura.Arquivos.Compartilhado;
using EAgenda.Infraestrutura.Arquivos.ModuloCategoria;
using EAgenda.Infraestrutura.Arquivos.ModuloDespesa;
using EAgenda.Infraestrutura.Arquivos.ModuloTarefa;
using EAgenda.Infraestrutura.SqlServer.ModuloContato;
using EAgenda.WebApp.ActionFilters;
using EAgenda.WebApp.DependencyInjection;
using EAgenda.Infraestrutura.SqlServer.ModuloCompromisso;

namespace EAgenda.WebApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews(options =>
        {
            options.Filters.Add<ValidarModeloAttribute>();
            options.Filters.Add<LogarAcaoAttribute>();
        });

        builder.Services.AddScoped<ContextoDados>((_) => new ContextoDados(true));
        builder.Services.AddScoped<IRepositorioCategoria, RepositorioCategoriaEmArquivo>();
        builder.Services.AddScoped<IRepositorioCompromisso, RepositorioCompromissoEmSql>();
        builder.Services.AddScoped<IRepositorioContato, RepositorioContatoEmSql>();
        builder.Services.AddScoped<IRepositorioDespesa, RepositorioDespesaEmArquivo>();
        builder.Services.AddScoped<IRepositorioTarefa, RepositorioTarefaEmArquivo>();

        builder.Services.AddSerilogConfig(builder.Logging);

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
            app.UseExceptionHandler("/erro");
        else
            app.UseDeveloperExceptionPage();

        app.UseAntiforgery();
        app.UseStaticFiles();
        app.UseHttpsRedirection();

        app.UseRouting();
        app.MapDefaultControllerRoute();

        app.Run();
    }
}
