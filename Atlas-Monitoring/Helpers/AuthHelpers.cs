using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Atlas_Monitoring.Helpers
{
    public class AuthHelpers
    {
        public string GenerateJWTToken(string userName)
        {

            var value = Environment.GetEnvironmentVariables();

            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Role, "AppUser")
            };
            var jwtToken = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddDays(30),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                       Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development" ? "6wE3sLgJQx42BKm9966hHwxS6wE3sLgJQx42BKm9966hHwxS6wE3sLgJQx42BKm9966hHwxS" : Environment.GetEnvironmentVariable("JWT_TOKEN"))
                        ),
                    SecurityAlgorithms.HmacSha256Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
