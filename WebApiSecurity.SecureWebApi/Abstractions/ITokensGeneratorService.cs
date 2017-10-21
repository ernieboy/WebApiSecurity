using System;
namespace WebApiSecurity.SecureWebApi.Abstractions
{
    public interface ITokensGeneratorService
    {
        string GenerateJwtTokenFromUsernameAndPassword(string username, string password);
    }
}
