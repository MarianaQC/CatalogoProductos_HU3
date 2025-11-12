using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using catalogoProductos.Application.Dto;
using catalogoProductos.Application.Interfaces;
using catalogoProductos.Domain.Entities;
using catalogoProductos.Domain.Interfaces;

namespace catalogoProductos.Application.Services
{
    // Servicio simple de autenticación: registro (hash) y login (devuelve JWT)
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly IConfiguration _configuration;

        // inyectamos repositorio de usuarios y configuración para leer la clave JWT
        public AuthService(IUserRepository userRepo, IConfiguration configuration)
        {
            _userRepo = userRepo;
            _configuration = configuration;
        }

        // Registro: valida existencia, hashea contraseña y guarda usuario
        public async Task<UserDto> RegisterAsync(RegisterDto dto)
        {
            // Verificar si ya existe username
            var existing = await _userRepo.GetByUserNameAsync(dto.UserName);
            if (existing != null)
                throw new InvalidOperationException("UserName already exists.");

            // Hashear password con BCrypt (por defecto suficiente para dev)
            var hashed = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            // Mapear a entidad
            var user = new User
            {
                UserName = dto.UserName,
                Email = dto.Email,
                Password = hashed,
                Role = dto.Role == "Admin" ? Role.Admin : Role.User
            };

            // Guardar
            var created = await _userRepo.AddAsync(user);

            // Devolver DTO sin password
            return new UserDto
            {
                Id = created.Id,
                UserName = created.UserName,
                Email = created.Email,
                Role = created.Role.ToString()
            };
        }

        // Login: verifica credenciales y devuelve token JWT
        public async Task<string?> LoginAsync(LoginDto dto)
        {
            // Buscar por username
            var user = await _userRepo.GetByUserNameAsync(dto.UserName);
            if (user == null)
            {
                // intentamos buscar por email si no existe por username
                // (opcional, pero útil)
                var list = await _userRepo.GetAllAsync();
                user = list.FirstOrDefault(u => u.Email == dto.UserName);
                if (user == null) return null;
            }

            // Verificar contraseña (BCrypt)
            bool valid = BCrypt.Net.BCrypt.Verify(dto.Password, user.Password);
            if (!valid) return null;

            // Generar JWT
            var jwtSection = _configuration.GetSection("Jwt");
            var key = jwtSection["Key"] ?? throw new InvalidOperationException("JWT Key not configured");
            var issuer = jwtSection["Issuer"] ?? "catalogoApi";
            var audience = jwtSection["Audience"] ?? "catalogoClient";
            var duration = int.TryParse(jwtSection["DurationInMinutes"], out var d) ? d : 60;

            var tokenHandler = new JwtSecurityTokenHandler();
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(duration),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
