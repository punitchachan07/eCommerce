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

        public IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}
