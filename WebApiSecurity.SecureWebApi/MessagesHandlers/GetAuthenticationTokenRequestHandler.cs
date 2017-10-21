using System;
using MediatR;
using WebApiSecurity.SecureWebApi.Abstractions;
using WebApiSecurity.SecureWebApi.Messages;

namespace WebApiSecurity.SecureWebApi.MessagesHandlers
{
    public class GetAuthenticationTokenRequestHandler : IRequestHandler<GetAuthenticationTokenRequest, string>
    {
        private readonly ITokensGeneratorService _tokensGeneratorService;

        public GetAuthenticationTokenRequestHandler(ITokensGeneratorService tokensGeneratorService)
        {
            _tokensGeneratorService = tokensGeneratorService;
        }

        public string Handle(GetAuthenticationTokenRequest message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            var token = _tokensGeneratorService.GenerateJwtTokenFromUsernameAndPassword(message.Username, message.Password);

            return token;
        }
    }
}
