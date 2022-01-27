using Domain.Interfaces;
using Domain.Model.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoDemo.Controllers.Base;
using System;

namespace ProjetoDemo.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : BaseController<IOrderComponent>
    {
        public OrderController([FromServices] IOrderComponent contract) : base(contract)
        {
        }

        [HttpPost]
        public IActionResult Create(OrderRequest request)
        {
            try
            {
                var responseMethod = ComponentCurrent.CreateOrder(request);
                return Ok(responseMethod);
            }
            catch (Exception err)
            {
                return BadRequest(err.Data);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetOrderById(int id)
        {
            try
            {
                var responseMethod = this.ComponentCurrent.GetOrderById(id);
                return Ok(responseMethod);
            }
            catch (Exception err)
            {
                return NotFound(err.Message);
            }
        }

        [HttpGet]
        [Route("customerOrders/{customerId}")]
        public IActionResult GetOrdersByCustomerId(int customerId)
        {
            try
            {
                var responseMethod = ComponentCurrent.GetOrdersByCustomerId(customerId);
                return Ok(responseMethod);
            }
            catch (Exception err)
            {
                return NotFound(err.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Remove(int id)
        {
            try
            {
                ComponentCurrent.RemoveOrder(id);
                return Ok("Order successfully removed");
            }
            catch (Exception err)
            {
                return NotFound(err.Message);
            }
        }
    }
}
