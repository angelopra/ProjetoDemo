using Domain.Model.Request;
using Domain.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAuthenticationComponent
    {
        TokenResponse ValidateCredentials(AuthenticationRequest request);
        public UserCreateResponse Create(UserCreateRequest request);
    }
}
