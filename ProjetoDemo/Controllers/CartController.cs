using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces;
using System;
using Domain.Model.Request;
using Domain.Entities.Base;

namespace ProjetoDemo.Controllers.Base
{
    [Route("api/[controller]")]
    public class CartController : BaseController<ICartComponent>
    {
        public CartController([FromServices] ICartComponent contract) : base(contract)
        {
        }

        [HttpPost]
        public IActionResult Create(CartRequest request)
        {
            try
            {
                var responseMethod = this.ComponentCurrent.AddCart(request);
                return Ok(responseMethod);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetCartById(int id)
        {
            try
            {
                var responseMethod = this.ComponentCurrent.GetCartById(id);
                return Ok(responseMethod);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public Cart Update(CartRequest request, int id)
        {
            var responseMethod = this.ComponentCurrent.Update(request, id);
            return responseMethod;
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
                return BadRequest(err.Message);
            }
        }

    }
}
