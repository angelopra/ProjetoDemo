using Microsoft.AspNetCore.Mvc;
using ProjetoDemo.Controllers.Base;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Domain.Model.Request;
using System;
using MediatR;
using System.Threading.Tasks;
using Domain.Model.Response;

namespace ProjetoDemo.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : BaseControllerMediator
    {
        public AuthenticationController(IHelper helper) : base(helper)
        {
        }

        [HttpPost]
        public async Task<ActionResult<TokenResponse>> AuthenticationLogin(AuthenticationRequest request)
        {
            try
            {
                var response = await Mediator.Send(request);
                return Ok(response);
            }
            catch (Exception err)
            {
                return BadRequest(err.Data);
            }
        }
    }
}
