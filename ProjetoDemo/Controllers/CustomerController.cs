using Domain.Entities;
using Domain.Interfaces;
using Domain.Model.Request;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoDemo.Controllers.Base;
using System;

namespace ProjetoDemo.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : BaseController<ICustomerComponent>
    {
        public CustomerController([FromServices] ICustomerComponent contract, IMediator mediator) : base(contract, mediator)
        {
        }

        [HttpPost]
        public IActionResult Create(CustomerRequest request)
        {
            try
            {
                var responseMethod = this.ComponentCurrent.AddCustomer(request);
                return Ok(responseMethod);
            }
            catch (Exception err)
            {
                return BadRequest(err.Data);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetCostumerById(int id)
        {
            try
            {
                var responseMethod = this.ComponentCurrent.GetCostumerById(id);
                return Ok(responseMethod);
            }
            catch (Exception err)
            {
                return NotFound(err.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("/login")]
        public IActionResult Login([FromBody]CustomerLoginRequest customer)
        {
            try
            {
                var token = ComponentCurrent.Login(customer);
                return Ok(token);
            }
            catch (Exception err)
            {

                return BadRequest(err);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(CustomerRequest request, int id)
        {
            try
            {
                var responseMethod = this.ComponentCurrent.Update(request, id);
                return Ok(responseMethod);
            }
            catch (Exception err)
            {
                return BadRequest(err.Data);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Remove(int id)
        {
            try
            {
                this.ComponentCurrent.Remove(id);
                return Ok();
            }
            catch (Exception err)
            {
                return NotFound(err.Message);
            }
        }
    }
}
