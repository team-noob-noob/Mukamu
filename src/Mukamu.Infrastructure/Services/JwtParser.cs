using Newtonsoft.Json;
using Mukamu.Application.Interfaces;

namespace Mukamu.Infrastructure.Services
{
    public class JwtParser : IJwtParser
    {
        public JwtBody ParseJwtString(string jwtString)
        {
            var jwtBodyString = jwtString.Split(".")[1];
            return JsonConvert.DeserializeObject<JwtBody>(jwtBodyString);
        }
    }
}
