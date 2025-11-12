using catalogoProductos.Application.Dto;

namespace catalogoProductos.Application.Interfaces
{
    // Interfaz simple que expone Register y Login
    public interface IAuthService
    {
        // Registra usuario y devuelve UserDto (sin password)
        Task<UserDto> RegisterAsync(RegisterDto dto);

        // Intenta loguear y devuelve JWT si es correcto (o null si falla)
        Task<string?> LoginAsync(LoginDto dto);
    }
}