using Domain.Entities;
using Domain.Interfaces;
using Domain.Model.Request;
using Domain.Model.Request.CategoryRequests;
using Domain.Model.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoDemo.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoDemo.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseControllerMediator
    {
        public CategoryController(IHelper helper) : base(helper)
        {
        }

        [HttpPost]
        public async Task<ActionResult<CategoryResponse>> Create(PostCategoryRequest request)
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

        //[HttpPost]
        //public IActionResult Create(CategoryRequest request)
        //{
        //    try
        //    {
        //        var responseMethod = this.ComponentCurrent.AddCategory(request);
        //        return Ok(responseMethod);
        //    }
        //    catch (Exception err)
        //    {
        //        return BadRequest(err.Data);
        //    }
        //}

        //[HttpPost]
        //[Route("ListCategorys")]
        //public List<Category> ListCategorys(CategoryQueryRequest request)
        //{
        //    var responseMethod = this.ComponentCurrent.GetCategorys(request);
        //    return responseMethod;
        //}

        //[HttpPut]
        //[Route("{id}")]
        //public IActionResult Update(CategoryRequest request, int id)
        //{
        //    try
        //    {
        //        var responseMethod = this.ComponentCurrent.Update(request, id);
        //        return Ok(responseMethod);
        //    }
        //    catch (Exception err)
        //    {
        //        return BadRequest(err.Data);
        //    }
        //}

        //[HttpDelete]
        //[Route("{id}")]
        //public IActionResult Remove(int id)
        //{
        //    try
        //    {
        //        this.ComponentCurrent.Remove(id);
        //        return Ok();
        //    }
        //    catch (Exception err)
        //    {
        //        return NotFound(err.Message);
        //    }
        //}

        //[HttpGet]
        //[Route("{id}")]
        //public IActionResult GetCategoryById(int id)
        //{
        //    try
        //    {
        //        var responseMethod = this.ComponentCurrent.GetCategoryById(id);
        //        return Ok(responseMethod);
        //    }
        //    catch (Exception err)
        //    {
        //        return NotFound(err.Message);
        //    }
        //}

        //[HttpGet]
        //[Route("allcategories")]
        //public IActionResult GetAllCategories(int? pageNumber, int? pageSize)
        //{
        //    try
        //    {
        //        var responseMethod = ComponentCurrent.GetAllCategories(pageNumber, pageSize);
        //        return Ok(responseMethod);
        //    }
        //    catch (Exception err)
        //    {
        //        return NotFound(err.Message);
        //    }
        //}
    }
}
