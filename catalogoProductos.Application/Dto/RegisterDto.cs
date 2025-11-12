namespace catalogoProductos.Application.Dto
{
    // DTO que recibimos al registrar un usuario
    public class RegisterDto
    {
        public string UserName { get; set; } = string.Empty; // nombre de usuario para login
        public string Email { get; set; } = string.Empty;    // email
        public string Password { get; set; } = string.Empty; // password en texto (se hashea antes de guardar)
        public string Role { get; set; } = "User";           // rol opcional (User o Admin)
    }
}