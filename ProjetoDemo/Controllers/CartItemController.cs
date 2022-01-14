﻿using Microsoft.AspNetCore.Mvc;
using ProjetoDemo.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Create(CartItemRequest request)
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
        [Route("{idCart}/{idProduct}")]
        public IActionResult GetCartById(int idCart, int idProduct)
        {
            try
            {
                var responseMethod = ComponentCurrent.GetCartItemById(idCart, idProduct);
                return Ok(responseMethod);
            }
            catch (Exception err)
            {
                return NotFound(err.Message);
            }
        }

        [HttpPut]
        [Route("{idCart}/{idProduct}")]
        public IActionResult Update(CartItemRequest request, int idCart, int idProduct)
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