using Domain.Entities;
using Domain.Interfaces;
using Domain.Model.Request;
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
        //[HttpPost]
        //public IActionResult Create(ProductRequest request)
        //{
        //    try
        //    {
        //        var responseMethod = ComponentCurrent.AddProduct(request);
        //        return Ok(responseMethod);
        //    }
        //    catch (Exception err)
        //    {
        //        return BadRequest(err.Data);
        //    }
        //}

        [HttpPost]
        public async Task<ActionResult<int>> CreateWMediator(ProductAddRequest request)
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

        //[HttpGet]
        //[Route("{id}")]
        //public IActionResult GetProductById(int id)
        //{
        //    try
        //    {
        //        var responseMethod = ComponentCurrent.GetProductById(id);
        //        return Ok(responseMethod);
        //    }
        //    catch (Exception err)
        //    {
        //        return NotFound(err.Message);
        //    }
        //}

        //[HttpGet]
        //[Route("AllProducts/{categoryId}")]
        //public IActionResult GetProductsByCategoryId(int categoryId, int? pageNumber, int? pageSize)
        //{
        //    try
        //    {
        //        var responseMethod = ComponentCurrent.GetProductsByCategoryId(categoryId, pageNumber, pageSize);
        //        return Ok(responseMethod);
        //    }
        //    catch (Exception err)
        //    {
        //        return NotFound(err.Message);
        //    }
        //}

        //[HttpPut]
        //[Route("{id}")]
        //public IActionResult Update(ProductRequest request, int id)
        //{
        //    try
        //    {
        //        var responseMethod = ComponentCurrent.Update(request, id);
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
        //        return Ok("Product successfully removed");
        //    }
        //    catch (Exception err)
        //    {
        //        return NotFound(err.Message);
        //    }
        //}
    }
}
