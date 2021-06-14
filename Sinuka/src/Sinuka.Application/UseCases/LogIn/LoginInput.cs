using System;

namespace Sinuka.Application.UseCases.Login
{
    public class LoginInput
    {
        public string username { get; set; }
        public string password { get; set; }
        public Guid ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientSecret { get; set; }
    }
}
