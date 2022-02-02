using Microsoft.AspNetCore.Mvc;
using ProjetoDemo.Controllers.Base;
using Domain.Interfaces;
using System;
using Domain.Model.Request;
using System.Collections.Generic;
using Domain.Model.Response;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace ProjetoDemo.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : BaseController<ICartItemComponent>
    {
        public CartItemController([FromServices] ICartItemComponent contract) : base(contract)
        {
        }

        [HttpPost]
        public IActionResult Create([FromBody]CartItemRequest request)
        {
            try
            {
                var responseMethod = ComponentCurrent.AddCartItem(request);
                return Ok(responseMethod);
            }
            catch (Exception err)
            {
                return BadRequest(err.Data);
            }
        }

        [HttpGet]
        [Route("getitens/{idCart}/{pageNumber}")]
        public IActionResult GetCartItens(int idCart, int? pageNumber)
        {
            try
            {
                var response = ComponentCurrent.GetCartItens(idCart, pageNumber);
                return Ok(response);
            }
            catch (Exception err)
            {
                return NotFound(err.Message);
            }
        }

        [HttpGet]
        [Route("{idCart}/{idProduct}")]
        public IActionResult GetCartItem(int idCart, int idProduct)
        {
            try
            {
                var response = ComponentCurrent.GetCartItem(idCart, idProduct);
                return Ok(response);
            }
            catch (Exception err)
            {
                return NotFound(err.Message);
            }
        }

        [HttpPut]
        [Route("{idCart}/{idProduct}")]
        public IActionResult Update([FromBody]CartItemUpdateRequest request, int idCart, int idProduct)
        {
            try
            {
                var responseMethod = ComponentCurrent.Update(request, idCart, idProduct);
                return Ok(responseMethod);
            }
            catch (Exception err)
            {
                return BadRequest(err.Data);
            }
        }

        [HttpDelete]
        [Route("{idCart}/{idProduct}")]
        public IActionResult Remove(int idCart, int idProduct)
        {
            try
            {
                this.ComponentCurrent.Remove(idCart, idProduct);
                return Ok();
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }

    }
}
