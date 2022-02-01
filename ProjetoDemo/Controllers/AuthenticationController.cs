using Microsoft.AspNetCore.Mvc;
using ProjetoDemo.Controllers.Base;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Domain.Model.Request;
using System;

namespace ProjetoDemo.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : BaseController<IAuthenticationComponent>
    {
        public AuthenticationController([FromServices] IAuthenticationComponent contract) : base(contract)
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
