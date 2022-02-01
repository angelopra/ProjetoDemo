using DataBase.Repository.Base;
using Domain.Entities;
using Domain.Entities.Security;
using Domain.Model.Request;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Business.AuthenticationBusiness
{
    public class IdentityInitializer
    {
        private readonly TokenConfiguration _tokenConfigurations;
        private readonly AuthenticationContext _context;
        private readonly UserManager<AuthorizationUserDB> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityInitializer(
            TokenConfiguration tokenConfigurations,
            AuthenticationContext context,
            UserManager<AuthorizationUserDB> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _tokenConfigurations = tokenConfigurations;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            //List<IdentityResult> roles = new List<IdentityResult>();
            foreach (PropertyInfo property in typeof(AccessRoles).GetProperties())
            {
                if (!_roleManager.RoleExistsAsync(property.Name).Result)
                {
                    //roles.Add(_roleManager.CreateAsync(new IdentityRole(property.Name)).Result);
                    var result = _roleManager.CreateAsync(new IdentityRole(property.Name)).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception($"Erro durante a criação da role {property.Name}.");
                    }
                }
            }
        }

        public void Create(UserCreateRequest request)
        {
            var userFound = _userManager.FindByNameAsync(request.UserName).GetAwaiter().GetResult();
            if (userFound != null)
            {
                throw new Exception("Username already in use");
            }
            CreateUser(
                new AuthorizationUserDB()
                {
                    UserName = request.UserName,
                    Email = request.Email,
                    EmailConfirmed = true
                }, request.Password, request.Role);
        }

        private void CreateUser(
            AuthorizationUserDB user,
            string password,
            string initialRole = null)
        {
            if (_userManager.FindByNameAsync(user.UserName).Result == null)
            {
                var resultado = _userManager
                    .CreateAsync(user, password).Result;

                if (resultado.Succeeded &&
                    !String.IsNullOrWhiteSpace(initialRole))
                {
                    _userManager.AddToRoleAsync(user, initialRole).Wait();
                }
            }
        }
    }
}
