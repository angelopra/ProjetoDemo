using Domain.Entities;
using Domain.Interfaces;
using Domain.Model.Request;
using Domain.Model.Request.ProductRequests;
using Domain.Model.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoDemo.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoDemo.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseControllerMediator
    {

        [HttpPost]
        public async Task<ActionResult<int>> Create(PostProductRequest request)
        {
            try
            {
                var response = await Mediator.Send(request);
                return Ok(response);
            }
            catch (Exception err)
            {
                return NotFound(err.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            try
            {

                var request = new GetProductByIdRequest();
                request.id = id;
                var response = await Mediator.Send(request);
                return Ok(response);
            }
            catch (Exception err)
            {
                return NotFound(err.Message);
            }
        }

        [HttpGet]
        [Route("AllProducts/{categoryId}")]
        public async Task<ActionResult<ProductListResponse>> GetProductsByCategoryId(int categoryId, int? pageNumber, int? pageSize)
        {
            try
            {

                var request = new GetProductsByCategoryIdRequest();
                request.categoryId = categoryId;
                request.pageNumber = pageNumber;
                request.pageSize = pageSize;

                var responseMethod = await Mediator.Send(request);
                return Ok(responseMethod);
            }
            catch (Exception err)
            {
                return NotFound(err.Message);
            }
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<Product>> Update(UpdateProductRequest request, int id)
        {
            try
            {
                request.ProductId = id;
                var response = await Mediator.Send(request);
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

                var request = new RemoveProductRequest();
                var response = await Mediator.Send(request);
                return Ok("Product successfully removed");
            }
            catch (Exception err)
            {
                return NotFound(err.Message);
            }
        }
    }
}
