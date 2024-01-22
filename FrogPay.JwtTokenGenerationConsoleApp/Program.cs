using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FrogPay.JwtTokenGenerationConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var claims = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "NomeDoUsuario"), // Substitua pelo nome do usuário real
                // Adicione outras reivindicações conforme necessário
            });

            var secretKey = "SuaChaveSecreta"; // Substitua pela sua chave secreta real
            var issuer = "SeuIssuer"; // Substitua pelo seu issuer real
            var audience = "SuaAudience"; // Substitua pela sua audience real
            var expiryInMinutes = 5; // Defina o tempo de expiração do token em minutos

            var token = JwtTokenGenerator.GenerateToken(secretKey, issuer, audience, expiryInMinutes, claims);

            Console.WriteLine($"Token JWT gerado:\n{token}");
        }
    }

    public static class JwtTokenGenerator
    {
        public static string GenerateToken(string secretKey, string issuer, string audience, int expiryInMinutes, ClaimsIdentity claimsIdentity)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GenerateRandomKey(64)));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Audience = audience,
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddMinutes(expiryInMinutes),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        static string GenerateRandomKey(int length)
        {
            byte[] key = new byte[length];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(key);
            }
            return Convert.ToBase64String(key);
        }
    }
}
