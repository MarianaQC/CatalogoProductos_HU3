namespace catalogoProductos.Application.Dto
{
    // DTO que devolvemos al cliente (nunca incluir password)
    public class UserDto
    {
        public int Id { get; set; }              // id del usuario
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = "User";
    }
}
