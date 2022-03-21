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

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<CategoryResponse>> Update(CategoryRequest request, int id)
        {
            try
            {
                var req = _helper.MappingEntity<UpdateCategoryRequest>(request);
                req.Id = id;
                var response = await Mediator.Send(req);
                return Ok(response);
            }
            catch (Exception err)
            {
                return BadRequest(err.Data);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<int>> Remove(int id)
        {
            try
            {
                var request = new RemoveCategoryRequest(id);
                var response = await Mediator.Send(request);
                return Ok($"Category {id} successfully removed");
            }
            catch (Exception err)
            {
                return NotFound(err.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<CategoryResponse>> GetCategoryById(int id)
        {
            try
            {
                var request = new GetCategoryByIdRequest(id);
                var response = await Mediator.Send(request);
                return Ok(response);
            }
            catch (Exception err)
            {
                return NotFound(err.Message);
            }
        }

        [HttpGet]
        [Route("allcategories")]
        public async Task<ActionResult<CategoryResponse>> GetAllCategories(int? pageNumber, int? pageSize)
        {
            try
            {
                var request = new GetAllCategoriesRequest(pageNumber, pageSize);

                var responseMethod = await Mediator.Send(request);
                return Ok(responseMethod);
            }
            catch (Exception err)
            {
                return NotFound(err.Message);
            }
        }
    }
}
