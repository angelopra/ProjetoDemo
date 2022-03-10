using Business.Base;
using Domain.Common;
using Domain.Entities.Security;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Model.Request;
using Domain.Model.Response;
using Domain.Validators;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Business.AuthenticationBusiness
{
    public class AuthenticationComponent : BaseBusinessComon,  IAuthenticationComponent
    {
        private List<ValidateError> errors;
        private UserManager<AuthorizationUserDB> _userManager;
        private SignInManager<AuthorizationUserDB> _signInManager;
        private SigningConfiguration _signingConfigurations;
        private TokenConfiguration _tokenConfigurations;
        private readonly RoleManager<IdentityRole> _roleManager;


        public AuthenticationComponent(
            SigningConfiguration signingConfigurations,
            TokenConfiguration tokenConfigurations,
            RoleManager<IdentityRole> roleManager,
            SignInManager<AuthorizationUserDB> signInManager,
            UserManager<AuthorizationUserDB> userManager)
        {
            _signInManager = signInManager;
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public TokenResponse ValidateCredentials(AuthenticationRequest request)
        {
            errors = null;
            try
            {
                string userRole = "";
                errors = ValidateObj<AuthenticationValidator>(request);
                if (errors != null)
                {
                    throw new Exception();
                }

                var userIdentity = _userManager.FindByNameAsync(request.UserName).GetAwaiter().GetResult();

                if (userIdentity != null)
                {
                    var makeLogin = _signInManager.CheckPasswordSignInAsync(userIdentity, request.Password, false).GetAwaiter().GetResult();
                    if (makeLogin.Succeeded)
                    {
                        var getUserRole = _userManager.GetRolesAsync(userIdentity).GetAwaiter().GetResult();
                        userRole = getUserRole.FirstOrDefault();
                    }
                    else
                    {
                        throw new Exception("Não foi possível fazer o login");
                    }
                }
                else
                {
                    throw new Exception("Não foi possível fazer o login");
                }

                return GenerateToken(request, userRole);
            }
            catch (Exception err)
            {
                err = MapperException(err, errors);
                throw;
            }
            
        }
        private TokenResponse GenerateToken(AuthenticationRequest request, string userRole)
        {
            try
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(request.UserName, "Login"),
                    new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, request.UserName),
                        new Claim(ClaimTypes.Role, userRole)
                    }
                );

                DateTime createdDate = DateTime.Now;
                DateTime expirationDate = createdDate + TimeSpan.FromHours(_tokenConfigurations.Hours);

                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = _tokenConfigurations.Issuer,
                    Audience = _tokenConfigurations.Audience,
                    SigningCredentials = _signingConfigurations.SigningCredentials,
                    Subject = identity,
                    NotBefore = createdDate,
                    Expires = expirationDate
                });
                var token = handler.WriteToken(securityToken);

                return new TokenResponse()
                {
                    Authenticated = true,
                    Created = createdDate,
                    Expiration = expirationDate,
                    AccessToken = token
                };
            }
            catch (Exception err)
            {
                err = MapperException(err);
                throw err;
            }
        }

        public UserCreateResponse Create(UserCreateRequest request)
        {
            try
            {
                var userFound = _userManager.FindByNameAsync(request.UserName).GetAwaiter().GetResult();
                if (userFound != null)
                {
                    throw new Exception("Username already in use");
                }

                var user = CreateUser(
                    new AuthorizationUserDB()
                    {
                        UserName = request.UserName,
                        Email = request.Email,
                        EmailConfirmed = true
                    }, request.Password, EnumGetValue(request.Role));

                var userMapped =  user.Map<UserCreateResponse>();
                userMapped.Role = _userManager.GetRolesAsync(user).GetAwaiter().GetResult().FirstOrDefault();

                return userMapped;
            }
            catch (Exception err)
            {
                err = MapperException(err);
                throw err;
            }
            
        }

        private AuthorizationUserDB CreateUser(
            AuthorizationUserDB user,
            string password,
            string initialRole = null)
        {
            try
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
                    else
                    {
                        var errors = String.Join(" ", resultado.Errors.Select(e => e.Description));
                        throw new Exception(errors);
                    }
                }
                return user;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public UserCreateResponse GetUser(string userName)
        {
            try
            {
                var user = _userManager.FindByNameAsync(userName).GetAwaiter().GetResult();
                if (user == null)
                {
                    throw new Exception("User not found");
                }
                var response = user.Map<UserCreateResponse>();
                response.Role = _userManager.GetRolesAsync(user).GetAwaiter().GetResult().FirstOrDefault();

                return response;
            }
            catch
            {
                throw;
            }
        }

        public PaginatedList<UserCreateResponse> GetAllUsers(int? pageNumber, int? pageSize)
        {
            var users = _userManager.Users.Paginate(pageNumber, pageSize);
            var response = users.Map<PaginatedList<UserCreateResponse>>();
            var list = new List<UserCreateResponse>();

            //foreach (var user in userMapped.Items)
            //{
            //    //response[i++].Role = _userManager.GetRolesAsync(user).GetAwaiter().GetResult().FirstOrDefault();
            //    user.Role = _userManager.GetRolesAsync(user).GetAwaiter().GetResult().FirstOrDefault();
            //    list.Add(user);
            //}
            return response;
        }

        public void DeleteUser(string userName)
        {
            try
            {
                var user = _userManager.FindByNameAsync(userName).GetAwaiter().GetResult();
                if (user == null)
                {
                    throw new Exception("User not found");
                }
                _userManager.DeleteAsync(user);
            }
            catch
            {
                throw;
            }
        }
    }
}
