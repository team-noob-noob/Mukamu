using System;

namespace Sinuka.Infrastructure.Configurations
{
    public class EmailConfig
    {
        public static string Username = Environment.GetEnvironmentVariable("SINUKA_EMAIL_USERNAME");
        public static string Password = Environment.GetEnvironmentVariable("SINUKA_EMAIL_PASSWORD");
    }
}
