using Domain.Entities;
using Domain.Interfaces;
using Domain.Model.Request;
using Domain.Model.Request.CustomerRequests;
using Domain.Model.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoDemo.Controllers.Base;
using System;
using System.Threading.Tasks;

namespace ProjetoDemo.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : BaseControllerMediator
    {
        public CustomerController(IHelper helper) : base(helper)
        {
        }

        [HttpPost]
        public async Task<ActionResult<CustomerResponse>> Create(PostCustomerRequest request)
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

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Customer>> GetCustomerById(int id)
        {
            try
            {

                var request = new GetCustomerByIdRequest();
                request.id = id;
                var response = await Mediator.Send(request);
                return Ok(response);
            }
            catch (Exception err)
            {
                return NotFound(err.Message);
            }
        }

        //[AllowAnonymous]
        //[HttpPost]
        //[Route("/login")]
        //public IActionResult Login([FromBody]CustomerLoginRequest customer)
        //{
        //    try
        //    {
        //        var token = ComponentCurrent.Login(customer);
        //        return Ok(token);
        //    }
        //    catch (Exception err)
        //    {

        //        return BadRequest(err);
        //    }
        //}

        //[HttpPut]
        //[Route("{id}")]
        //public IActionResult Update(CustomerRequest request, int id)
        //{
        //    try
        //    {
        //        var responseMethod = this.ComponentCurrent.Update(request, id);
        //        return Ok(responseMethod);
        //    }
        //    catch (Exception err)
        //    {
        //        return BadRequest(err.Data);
        //    }
        //}

        //[HttpDelete]
        //[Route("{id}")]
        //public IActionResult Remove(int id)
        //{
        //    try
        //    {
        //        this.ComponentCurrent.Remove(id);
        //        return Ok();
        //    }
        //    catch (Exception err)
        //    {
        //        return NotFound(err.Message);
        //    }
        //}
    }
}
