using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace WebApiSecurity.SecureWebApi.Messages
{
    public class GetAuthenticationTokenRequest : IRequest<string>
    {

        [Required]
        [MaxLength(10)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
