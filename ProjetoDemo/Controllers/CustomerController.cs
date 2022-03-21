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

                var request = new GetCustomerByIdRequest(id);
                var response = await Mediator.Send(request);
                return Ok(response);
            }
            catch (Exception err)
            {
                return NotFound(err.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("/login")]
        public async Task<ActionResult<CustomerResponse>> Login([FromBody] LoginCustomerRequest request)
        {
            try
            {
                var token = await Mediator.Send(request);
                return Ok(token);
            }
            catch (Exception err)
            {

                return BadRequest(err);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<CustomerResponse>> Update(CustomerRequest request, int id)
        {
            try
            {
                var req = _helper.MappingEntity<UpdateCustomerRequest>(request);
                req.Id = id;
                var response = await Mediator.Send(req);
                return Ok(response);
            }
            catch (Exception err)
            {
                return BadRequest(err.Data);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<int>> Remove(int id)
        {
            try
            {
                var request = new RemoveCustomerRequest(id);
                var response = await Mediator.Send(request);
                return Ok($"Customer {id} successfully removed");
            }
            catch (Exception err)
            {
                return NotFound(err.Message);
            }
        }
    }
}
