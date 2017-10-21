using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApiSecurity.SecureWebApi.Abstractions;

namespace WebApiSecurity.SecureWebApi.Implementations
{
    public class TokensGeneratorService : ITokensGeneratorService
    {
        private readonly IConfiguration _configuration;

        public TokensGeneratorService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwtTokenFromUsernameAndPassword(string username, string password)
        {
            if (username == null) throw new ArgumentNullException(nameof(username));
            if (password == null) throw new ArgumentNullException(nameof(password));

            //In a real world scenario validate credentials against some data store here before proceeding

            var claims = new[]
            {
                    new Claim(JwtRegisteredClaimNames.Sub, username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("HasProtectedResourceAccessRights", bool.TrueString)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Tokens:Issuer"],
                                             _configuration["Tokens:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(30),
              notBefore: DateTime.Now,
              signingCredentials: creds);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }
    }
}
