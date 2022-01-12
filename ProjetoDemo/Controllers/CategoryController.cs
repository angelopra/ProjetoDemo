using Domain.Entities;
using Domain.Interfaces;
using Domain.Model.Request;
using Domain.Model.Response;
using Microsoft.AspNetCore.Mvc;
using ProjetoDemo.Controllers.Base;
using System.Collections.Generic;

namespace ProjetoDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseController<ICategoryComponent>
    {
        public CategoryController([FromServices] ICategoryComponent contract) : base(contract)
        {
        }

        [HttpPost]
        public int Create(CategoryRequest request)
        {
            var responseMethod = this.ComponentCurrent.AddCategory(request);
            return responseMethod;
        }

        [HttpPost]
        [Route("ListCategorys")]
        public List<Category> ListCategorys(CategoryQueryRequest request)
        {
            var responseMethod = this.ComponentCurrent.GetCategorys(request);
            return responseMethod;
        }

        [HttpPut]
        [Route("{id}")]
        public Category Update(CategoryRequest request, int id)
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

        [HttpGet]
        [Route("{id}")]
        public Category GetCategoryById(int id)
        {
            var responseMethod = this.ComponentCurrent.GetCategoryById(id);
            return responseMethod;
        }
    }
}
