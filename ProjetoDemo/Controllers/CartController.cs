using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces;
using System;
using Domain.Model.Request;
using Domain.Entities.Base;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using System.Threading.Tasks;
using Domain.Model.Request.CartRequests;
using Domain.Model.Response;

namespace ProjetoDemo.Controllers.Base
{
    //[Authorize(Roles = "Admin,User")]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : BaseControllerMediator
    {
        public CartController(IHelper helper) : base(helper)
        {
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody]PostCartRequest request)
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
        public async Task<ActionResult<CartResponse>> GetCartById(int id)
        {
            try
            {
                var request = new GetCartRequest();
                request.id = id;
                var response = await Mediator.Send(request);
                return Ok(response);
            }
            catch (Exception err)
            {
                return NotFound(err.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<CartResponse>> Update([FromBody]CartRequest req, int id)
        {
            try
            {
                var request = _helper.MappingEntity<UpdateCartRequest>(req);
                request.IdCart = id;

                var response = await Mediator.Send(request);
                return Ok(response);
            }
            catch (Exception err)
            {
                return BadRequest(err.Data);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<CartResponse>> Remove(int id)
        {
            try
            {
                var request = new RemoveCartRequest();
                request.Id = id;
                var response = await Mediator.Send(request);
                return Ok(response);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpDelete]
        [Route("/reset/{id}")]
        public async Task<ActionResult<int>> RemoveAllItems(int id)
        {
            try
            {
                var request = new RemoveAllItemsRequest();
                request.Id = id;
                var response = await Mediator.Send(request);
                return Ok(response);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }

    }
}
