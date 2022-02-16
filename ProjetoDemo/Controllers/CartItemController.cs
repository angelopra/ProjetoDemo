using Microsoft.AspNetCore.Mvc;
using ProjetoDemo.Controllers.Base;
using Domain.Interfaces;
using System;
using Domain.Model.Request;
using System.Collections.Generic;
using Domain.Model.Response;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using System.Threading.Tasks;
using System.Collections;
using Domain.Model.Request.CartItemRequests;

namespace ProjetoDemo.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : BaseControllerMediator
    {
        [HttpPost]
        public async Task<ActionResult<CartItemModelResponse>> Create([FromBody] CartItemRequest request)
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
        [Route("getitens/{idCart}")]
        public async Task<ActionResult<IEnumerable>> GetCartItens(int idCart, int? pageNumber, int? pageSize)
        {
            try
            {
                var request = new GetCartItensRequest();
                request.idCart = idCart;
                request.pageSize = pageSize;
                request.pageNumber = pageNumber;

                var response = await Mediator.Send(request);
                return Ok(response);
            }
            catch (Exception err)
            {
                return NotFound(err.Message);
            }
        }

        [HttpGet]
        [Route("{idCart}/{idProduct}")]
        public async Task<ActionResult<CartItemModelResponse>> GetCartItem(int idCart, int idProduct)
        {
            try
            {
                var request = new GetCartItemRequest();
                request.idProduct = idProduct;
                request.idCart = idCart;

                var response = await Mediator.Send(request);
                return Ok(response);
            }
            catch (Exception err)
            {
                return NotFound(err.Message);
            }
        }

        [HttpPut]
        [Route("{idCart}/{idProduct}")]
        public async Task<ActionResult<CartItemModelResponse>> Update([FromBody]CartItemUpdateRequest request, int idCart, int idProduct)
        {
            try
            {
                request.IdCart = idCart;
                request.IdProduct = idProduct;
                var response = await Mediator.Send(request);
                return Ok(response);
            }
            catch (Exception err)
            {
                return BadRequest(err.Data);
            }
        }

        [HttpDelete]
        [Route("{idCart}/{idProduct}")]
        public async Task<IActionResult> Remove(int idCart, int idProduct)
        {
            try
            {
                var request = new RemoveCartItemRequest();
                request.idProduct = idProduct;
                request.idCart = idCart;
                var response = await Mediator.Send(request);
                return Ok();
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }

    }
}
