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
    }
}
