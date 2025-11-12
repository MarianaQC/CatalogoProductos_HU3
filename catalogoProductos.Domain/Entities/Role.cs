namespace catalogoProductos.Domain.Entities;

// Enumeración simple para roles. Solo dos roles necesarios.
public enum Role
{
    User = 0, // rol normal
    Admin = 1 // rol administrador (puede eliminar)
}



// Por qué?
// El HU3 pide roles básicos (Admin y User).
// Usar enum hace validaciones simples y evita
// tablas extra, para no tener que normalizar roles.