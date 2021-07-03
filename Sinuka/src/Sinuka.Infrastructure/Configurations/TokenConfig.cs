using System;

namespace Sinuka.Infrastructure.Configurations
{
    public class TokenConfig
    {
        public static int RefreshTokenLength = 300;
        public static TimeSpan RefreshTokenLifetimeLength = new TimeSpan(30, 0, 0, 0);
        public static TimeSpan TokenLifetimeLength = new TimeSpan(1, 0, 0);
        public static string Key 
            = Environment.GetEnvironmentVariable("SINUKA_TOKEN_KEY") 
            ?? "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
    }
}
