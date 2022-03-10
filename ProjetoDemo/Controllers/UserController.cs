using Business.AuthenticationBusiness;
using Domain.Interfaces;
using Domain.Model.Request;
using Domain.Model.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoDemo.Controllers.Base;
using System;

namespace ProjetoDemo.Controllers
{
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

        [HttpGet]
        [Route("{userName}")]
        public IActionResult GetUser(string userName)
        {
            try
            {
                var response = ComponentCurrent.GetUser(userName);
                return Ok(response);
            }
            catch (Exception err)
            {
                return NotFound(err.Data);
            }
        }

        [HttpGet]
        public IActionResult GetAllUsers(int? pageNumber, int? pageSize)
        {
            try
            {
                var response = ComponentCurrent.GetAllUsers(pageNumber, pageSize);
                return Ok(response);
            }
            catch (Exception err)
            {
                return NotFound(err.Data);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{userName}")]
        public IActionResult DeleteUser(string userName)
        {
            try
            {
                ComponentCurrent.DeleteUser(userName);
                return Ok("User removed successfully");
            }
            catch (Exception err)
            {
                return NotFound(err.Data);
            }
        }
    }
}
