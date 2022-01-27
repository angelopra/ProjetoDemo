using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces;
using System;
using Domain.Model.Request;
using Domain.Entities.Base;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace ProjetoDemo.Controllers.Base
{
    [Authorize]
    [Route("api/[controller]")]
    public class CartController : BaseController<ICartComponent>
    {
        public CartController([FromServices] ICartComponent contract) : base(contract)
        {
        }

        [HttpPost]
        public IActionResult Create([FromBody]CartRequest request)
        {
            try
            {
                var responseMethod = this.ComponentCurrent.AddCart(request);
                return Ok(responseMethod);
            }
            catch (Exception err)
            {
                return BadRequest(err.Data);
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
                return NotFound(err.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromBody]CartRequest request, int id)
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
                return BadRequest(err.Message);
            }
        }

        [HttpDelete]
        [Route("/reset/{id}")]
        public IActionResult RemoveAllItems(int id)
        {
            try
            {
                var numberDeleted = ComponentCurrent.RemoveAllItems(id);
                return Ok(numberDeleted);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }

    }
}
