using System.Text.Json;
using Microsoft.Data.SqlClient;

namespace cosc3380_group4_themepark
{
    public class Credentials
    {
        private class CredentialOptions
        {
            public String server { get; set; }
            public String db { get; set; }
            public String username { get; set; }
            public String password { get; set; }
        }

        public static String getConnectionString()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            using (StreamReader r = new StreamReader("db.json"))
            {
                String json = r.ReadToEnd();
                CredentialOptions? options = JsonSerializer.Deserialize<CredentialOptions>(json);
                if (options == null)
                {
                    throw new Exception();
                }
                builder.DataSource = options.server;
                builder.UserID = options.username;
                builder.Password = options.password;
                builder.InitialCatalog = options.db;
                builder.TrustServerCertificate = true;
            }
            return builder.ConnectionString;
        }
    }
}