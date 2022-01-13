using Domain.Entities;
using Domain.Interfaces;
using Domain.Model.Request;
using Microsoft.AspNetCore.Mvc;
using ProjetoDemo.Controllers.Base;

namespace ProjetoDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : BaseController<ICustomerComponent>
    {
        public CustomerController([FromServices] ICustomerComponent contract) : base(contract)
        {
        }

        [HttpPost]
        public int Create(CustomerRequest request)
        {
            var responseMethod = this.ComponentCurrent.AddCustomer(request);
            return responseMethod;
        }

        [HttpGet]
        [Route("{id}")]
        public Customer GetCostumerById(int id)
        {
            var responseMethod = this.ComponentCurrent.GetCostumerById(id);
            return responseMethod;
        }

        [HttpPut]
        [Route("{id}")]
        public Customer Update(CustomerRequest request, int id)
        {
            var responseMethod = this.ComponentCurrent.Update(request, id);
            return responseMethod;
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Remove(int id)
        {
            this.ComponentCurrent.Remove(id);
            return Ok();
        }
    }
}
