using EAgenda.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace EAgenda.WebApp.DependencyInjection;

public static class EntityFrameworkConfig
{
    public static void AddEntityFrameworkConfig(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connectionString = configuration["SQL_CONNECTION_STRING"];

        services.AddDbContext<EAgendaDbContext>(options =>
            options.UseSqlServer(connectionString));
    }
}
