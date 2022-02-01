using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Security
{
    public class SigningConfiguration
    {
        public SecurityKey Key { get; }
        public SigningCredentials SigningCredentials { get; }

        public SigningConfiguration(TokenConfiguration tokenConfiguration)
        {
            Key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(tokenConfiguration.SecretKey));

            SigningCredentials = new(
                Key, SecurityAlgorithms.HmacSha256Signature);
        }
    }
}
