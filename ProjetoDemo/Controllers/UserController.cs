using Business.AuthenticationBusiness;
using Domain.Model.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ProjetoDemo.Controllers
{
    [Authorize(Roles = "Admin,User")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController
    {
        private readonly IdentityInitializer _identityInitializer;
        public UserController(IdentityInitializer identityInitializer)
        {
            _identityInitializer = identityInitializer;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public void Create([FromBody] UserCreateRequest request)
        {
            try
            {
                _identityInitializer.Create(request);
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}
