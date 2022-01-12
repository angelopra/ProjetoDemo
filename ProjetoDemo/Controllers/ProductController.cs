using Domain.Entities;
using Domain.Interfaces;
using Domain.Model.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoDemo.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController<IProductComponent>
    {
        public ProductController([FromServices] IProductComponent contract) : base(contract)
        {

        }

        [HttpPost]
        public int Create(ProductRequest request)
        {
            var responseMethod = this.ComponentCurrent.AddProduct(request);
            return responseMethod;
        }

        [HttpGet]
        [Route("{id}")]
        public Product GetProductById(int id)
        {
            var responseMethod = this.ComponentCurrent.GetProductById(id);
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
