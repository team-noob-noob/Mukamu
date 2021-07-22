using System;

namespace Mukamu.Infrastructure.Configurations
{
    public class DbConfig
    {
        public static string DbConnectionString 
            = Environment.GetEnvironmentVariable("SINUKA_DB_CONN_STR")
            ?? "server=localhost;database=sinukaka;uid=root;pwd=root;";
        public static System.Version MySqlVersion = new System.Version(
            Environment.GetEnvironmentVariable("SINUKA_DB_VER_MAJ") is not null && int.TryParse(Environment.GetEnvironmentVariable("SINUKA_DB_VER_MAJ"), out int maj) ? maj : 5, 
            Environment.GetEnvironmentVariable("SINUKA_DB_VER_MIN") is not null && int.TryParse(Environment.GetEnvironmentVariable("SINUKA_DB_VER_MIN"), out int min) ? min : 7, 
            Environment.GetEnvironmentVariable("SINUKA_DB_VER_REV") is not null && int.TryParse(Environment.GetEnvironmentVariable("SINUKA_DB_VER_REV"), out int rev) ? rev : 32
        );
    }
}