namespace catalogoProductos.Domain.Entities;

// Entidad principal Usuario usada en todo el sistema.
// Notas: guardamos Password como string (hashed) — el hashing se hace en Application al crear usuario.
public class User
{
    public int Id { get; set; }                      // PK
    public string UserName { get; set; } = string.Empty; // nombre de usuario para login
    public string Email { get; set; } = string.Empty;    // email
    public string Password { get; set; } = string.Empty; // password hasheado
    public Role Role { get; set; } = Role.User;          // rol: User o Admin
}

// Por qué?
// Necesitamos registro/login con role; esto cubre lo mínimo.