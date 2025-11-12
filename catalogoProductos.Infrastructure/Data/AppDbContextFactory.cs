using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace catalogoProductos.Infrastructure.Data
{
    // Fábrica para crear instancias de AppDbContext en tiempo de diseño (migraciones)
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            // Cadena de conexión temporal solo para diseño
            var connectionString = "server=localhost;port=3306;database=catalogodb;user=root;password=2739425;";

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}