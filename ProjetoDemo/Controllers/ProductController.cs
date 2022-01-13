﻿using Domain.Entities;
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
        public IActionResult Create(ProductRequest request)
        {
            try
            {
                var responseMethod = this.ComponentCurrent.AddProduct(request);
                return Ok(responseMethod);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetProductById(int id)
        {
            try
            {
                var responseMethod = this.ComponentCurrent.GetProductById(id);
                return Ok(responseMethod);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(ProductRequest request, int id)
        {
            try
            {
                var responseMethod = this.ComponentCurrent.Update(request, id);
                return Ok(responseMethod);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
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
                return NotFound(err.Message);
            }
        }
    }
}
