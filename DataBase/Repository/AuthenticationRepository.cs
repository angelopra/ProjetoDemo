using DataBase.Context;
using DataBase.Repository.Base;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repository
{
    public class AuthenticationRepository : BaseRepository<AuthenticationContext>, IAuthenticationRepository
    {
        public AuthenticationRepository(AuthenticationContext context) : base(context)
        {
        }
    }
}
