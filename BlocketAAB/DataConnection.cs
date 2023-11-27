using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlocketAAB
{
    public class DataConnection
    {
        private static readonly IConfiguration Configuration;

        static DataConnection()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }

        public static IDbConnection GetDbConnection()
        {
            string connectionString = GetConnectionString();
            IDbConnection dbConnection = new SqlConnection(connectionString);
            return dbConnection;
        }

        public static string GetConnectionString()
        {
            try
            {
                string connectionString = Configuration.GetConnectionString("DefaultConnection");
                return connectionString;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"Error reading connection string: {ex.Message}");
                throw;
            }
        }
    }
}
