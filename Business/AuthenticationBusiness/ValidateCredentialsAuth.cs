using Business.Base;
using Domain.Entities.Security;
using Domain.Interfaces;
using Domain.Model.Request;
using Domain.Model.Response;
using Domain.Validators;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.AuthenticationBusiness
{
    public class ValidateCredentialsAuth : BaseBusinessComon, IRequestHandler<AuthenticationRequest, TokenResponse>
    {
        private List<ValidateError> errors;
        private UserManager<AuthorizationUserDB> _userManager;
        private SignInManager<AuthorizationUserDB> _signInManager;
        private SigningConfiguration _signingConfigurations;
        private TokenConfiguration _tokenConfigurations;
        private readonly RoleManager<IdentityRole> _roleManager;
        public ValidateCredentialsAuth(
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

        public async Task<TokenResponse> Handle(AuthenticationRequest request, CancellationToken cancellationToken)
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
    }
}
