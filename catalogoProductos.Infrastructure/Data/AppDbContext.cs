using Microsoft.EntityFrameworkCore;
using catalogoProductos.Domain.Entities;

namespace catalogoProductos.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    // DbSets
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;

    // Configuraciones simples de modelo
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Usuario: UserName y Email deben ser únicos
        modelBuilder.Entity<User>()
            .HasIndex(u => u.UserName)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        // Producto: Code único
        modelBuilder.Entity<Product>()
            .HasIndex(p => p.Code)
            .IsUnique();
    }
}

// Indices únicos para evitar duplicados en username/email/code.