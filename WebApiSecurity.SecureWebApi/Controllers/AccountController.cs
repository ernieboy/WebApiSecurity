using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiSecurity.SecureWebApi.Messages;
using WebApiSecurity.SecureWebApi.Utilities;

namespace WebApiSecurity.SecureWebApi.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator) => _mediator = mediator;


        [HttpPost("gettoken")]
        public async Task<IActionResult> ExchangeUsernameAndPasswordForToken([FromBody]GetAuthenticationTokenRequest request)
        {
            if (!ModelState.IsValid)
            {

                var errorListAux = (from m in ModelState
                                    where m.Value.Errors.Count() > 0
                                    select
                                       new ErrorDetail {
                                           FieldName = m.Key,
                    MessageList = (from msg in m.Value.Errors
                                                        select msg.ErrorMessage).ToArray()
                                       })
                     .AsEnumerable()
                     .ToDictionary(v => v.FieldName, v => v);

                var errors = Json(errorListAux);
                return BadRequest(errors);
            }

            var result = await _mediator.Send(request);
            return Content(result);
        }

        [Authorize(Policy = "RequireProtectedResourceAccessRights")]
        [HttpGet("protectedresouce")]
        public IActionResult ProtectedResource()
        {
            return Json(new {Result = "Access Granted!"});
        }

    }
}
