using catalogoProductos.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using catalogoProductos.Infrastructure.Data;
using catalogoProductos.Infrastructure.Repositories;

namespace catalogoProductos.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    // Método de extensión para configurar servicios de infraestructura
    // Registra AppDbContext y los repositorios en el contenedor de dependencias.
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Obtiene la cadena de conexión de la configuración o usa una por defecto.
        // En producción se recomienda usar variables de entorno.
        var connection = configuration.GetConnectionString("DefaultConnection") 
                         ?? "server=localhost;port=3306;database=catalogodb;user=root;password=2739425;";

        // Registra el DbContext usando MySQL (Pomelo).
        services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(connection, ServerVersion.AutoDetect(connection))
        );

        // Registra los repositorios en el contenedor de dependencias.
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}