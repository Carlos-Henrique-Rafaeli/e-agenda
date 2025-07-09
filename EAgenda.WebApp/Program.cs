using EAgenda.Dominio.ModuloCompromisso;
using EAgenda.Dominio.ModuloCategoria;
using EAgenda.Dominio.ModuloContato;
using EAgenda.Dominio.ModuloDespesa;
using EAgenda.Dominio.ModuloTarefa;
using EAgenda.WebApp.ActionFilters;
using EAgenda.WebApp.DependencyInjection;
using EAgenda.Infraestrutura.SqlServer.ModuloCompromisso;
using EAgenda.Infraestrutura.SqlServer.ModuloTarefa;
using EAgenda.Infraestrutura.SqlServer.ModuloDespesa;
using EAgenda.Infraestrutura.SqlServer.ModuloCategoria;
using Microsoft.Data.SqlClient;
using System.Data;
using EAgenda.Infraestrutura.Orm.ModuloContato;

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

        builder.Services.AddScoped<IDbConnection>(provider =>
        {
            var connectionString = builder.Configuration["SQL_CONNECTION_STRING"];

            return new SqlConnection(connectionString);
        });

        builder.Services.AddScoped<IRepositorioContato, RepositorioContatoEmOrm>();
        builder.Services.AddScoped<IRepositorioCategoria, RepositorioCategoriaEmSql>();
        builder.Services.AddScoped<IRepositorioCompromisso, RepositorioCompromissoEmSql>();
        builder.Services.AddScoped<IRepositorioDespesa, RepositorioDespesaEmSql>();
        builder.Services.AddScoped<IRepositorioTarefa, RepositorioTarefaEmSql>();

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
