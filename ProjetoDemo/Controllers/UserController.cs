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

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetUserById(string id)
        {
            try
            {
                var response = ComponentCurrent.GetUserById(id);
                return Ok();
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
        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateUser(UserUpdateRequest user, string id)
        {
            try
            {
                var response = ComponentCurrent.UpdateUser(user, id);
                return Ok(response);
            }
            catch (Exception err)
            {
                return BadRequest(err.Data);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteUser(string id)
        {
            try
            {
                ComponentCurrent.DeleteUser(id);
                return Ok("User removed successfully");
            }
            catch (Exception err)
            {
                return NotFound(err.Data);
            }
        }
    }
}
