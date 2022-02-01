using Business.AuthenticationBusiness;
using Domain.Interfaces;
using Domain.Model.Request;
using Domain.Model.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoDemo.Controllers.Base;
using System;

namespace ProjetoDemo.Controllers
{
    [Authorize(Roles = "Admin,User")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController<IAuthenticationComponent>
    {
        public UserController([FromServices] IAuthenticationComponent contract) : base(contract)
        {
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create([FromBody] UserCreateRequest request)
        {
            try
            {
                return Ok(ComponentCurrent.Create(request));
            }
            catch (Exception err)
            {
                return BadRequest(err.Data);
            }
        }
    }
}
