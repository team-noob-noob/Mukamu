using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Sinuka.Infrastructure.Utils;

namespace Sinuka.Infrastructure.Security
{
    public class JwtTokenGenerator
    {
        public static string GeneratorToken(object payload)
        {
            string key = Sinuka.Infrastructure.Configurations.TokenConfig.Key;

            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(key));

            var credentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(
                securityKey, 
                SecurityAlgorithms.HmacSha512Signature);

            var header = new JwtHeader(credentials);

            var tokenPayload = new JwtPayload();

            ObjectUtils.CopyValues(tokenPayload, payload);

            var secToken = new JwtSecurityToken(header, tokenPayload);
            var handler = new JwtSecurityTokenHandler();

            var tokenString = handler.WriteToken(secToken);

            return tokenString;
        }
    }
}
