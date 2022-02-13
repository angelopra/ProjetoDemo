using Microsoft.AspNetCore.Mvc;
using ProjetoDemo.Controllers.Base;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Domain.Model.Request;
using System;
using MediatR;

namespace ProjetoDemo.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : BaseController<IAuthenticationComponent>
    {
        public AuthenticationController([FromServices] IAuthenticationComponent contract, IMediator mediator) : base(contract, mediator)
        {
        }

        [HttpPost]
        public IActionResult AuthenticationLogin(AuthenticationRequest request)
        {
            try
            {
                var response = ComponentCurrent.ValidateCredentials(request);
                return Ok(response);
            }
            catch (Exception err)
            {
                return BadRequest(err.Data);
            }
        }
    }
}
