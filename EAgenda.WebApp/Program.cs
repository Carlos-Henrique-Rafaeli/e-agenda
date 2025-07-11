using EAgenda.Dominio.ModuloCompromisso;
using EAgenda.Dominio.ModuloCategoria;
using EAgenda.Dominio.ModuloContato;
using EAgenda.Dominio.ModuloDespesa;
using EAgenda.Dominio.ModuloTarefa;
using EAgenda.WebApp.ActionFilters;
using EAgenda.WebApp.DependencyInjection;
using EAgenda.Infraestrutura.SqlServer.ModuloTarefa;
using EAgenda.Infraestrutura.SqlServer.ModuloDespesa;
using Microsoft.Data.SqlClient;
using System.Data;
using EAgenda.Infraestrutura.Orm.ModuloContato;
using EAgenda.Infraestrutura.Orm.ModuloCompromisso;
using EAgenda.Infraestrutura.Orm.ModuloCategoria;
using EAgenda.Infraestrutura.Orm.ModuloDespesa;
using EAgenda.Infraestrutura.Orm.ModuloTarefa;

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

        builder.Services.AddEntityFrameworkConfig(builder.Configuration);

        builder.Services.AddScoped<IRepositorioContato, RepositorioContatoEmOrm>();
        builder.Services.AddScoped<IRepositorioCompromisso, RepositorioCompromissoEmOrm>();
        builder.Services.AddScoped<IRepositorioCategoria, RepositorioCategoriaEmOrm>();
        builder.Services.AddScoped<IRepositorioDespesa, RepositorioDespesaEmOrm>();
        builder.Services.AddScoped<IRepositorioTarefa, RepositorioTarefaEmOrm>();

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
