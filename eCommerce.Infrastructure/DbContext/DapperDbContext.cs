using System.Data;
using Npgsql;
using Microsoft.Extensions.Configuration;

namespace eCommerce.Infrastructure.DbContext
{
    public class DapperDbContext
    {
        private readonly string _connectionString;

        public DapperDbContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PostgresConnection") ?? string.Empty;
        }

        // Dapper connection. 
        // This method creates a new NpgsqlConnection using the connection string from the configuration.

        public IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}
