using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiSecurity.SecureWebApi.Messages;

namespace WebApiSecurity.SecureWebApi.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("gettoken")]
        public async Task<IActionResult> ExchangeUsernameAndPasswordForToken(
            [FromBody]GetAuthenticationTokenRequest request)
        {
            var result = await _mediator.Send(request);
            return Content(result);
        }

        [Authorize(Policy = "RequireProtectedResourceAccessRights")]
        [HttpGet("protectedresouce")]
        public IActionResult ProtectedResource()
        {
            return Json(new { Result = "Access Granted!" });
        }

    }
}
