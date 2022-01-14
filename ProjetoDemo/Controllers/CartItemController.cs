﻿using Microsoft.AspNetCore.Mvc;
using ProjetoDemo.Controllers.Base;
using Domain.Interfaces;
using System;
using Domain.Model.Request;

namespace ProjetoDemo.Controllers
{
    [Route("api/[controller]")]
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
                return BadRequest(err.Message);
            }
        }

        [HttpGet]
        [Route("{idCart}")]
        public IActionResult GetCartItemsByCartId(int idCart) // trocar isso aqui por uma lista de produtos
        {
            try
            {
                var responseMethod = ComponentCurrent.GetCartItemsByCartId(idCart);
                return Ok(responseMethod);
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
                return BadRequest(err.Message);
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
