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
        public UserCreateResponse GetUserById(string id);
        public List<UserCreateResponse> GetAllUsers(int? pageNumber, int? pageSize);
        public UserCreateResponse UpdateUser(UserUpdateRequest userRequest, string id);
        public void DeleteUser(string id);
    }
}
