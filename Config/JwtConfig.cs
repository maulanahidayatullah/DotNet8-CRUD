using Dotnet_AnimeCRUD.Model.Entity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Dotnet_AnimeCRUD.Config
{
    public class JwtConfig
    {

        private readonly IConfiguration _config;
        public JwtConfig(IConfiguration _config)
        {
            this._config = _config;
        }

        public string Generate(User user)
        {
            // Ambil config "JwtSettings" yang ada di appsettings.json
            var jwtSettings = _config.GetSection("JwtSettings");

            // Buat array data apa saja yang akan dimasukan ke token
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role?.Rolename ?? "Romam") // Memasukan relasi Role ke ClaimTypes.Role
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
