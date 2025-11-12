namespace catalogoProductos.Application.Dto
{
    // DTO para login
    public class LoginDto
    {
        public string UserName { get; set; } = string.Empty; // nombre de usuario o email
        public string Password { get; set; } = string.Empty; // password en texto
    }
}