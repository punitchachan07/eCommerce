using Dapper;
using eCommerce.Core.DTO;
using eCommerce.Core.Entities;
using eCommerce.Core.RepositoryContracts;
using eCommerce.Infrastructure.DbContext;

namespace eCommerce.Infrastructure.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly DapperDbContext _dapperDbContext;

    public UsersRepository(DapperDbContext dapperDbContext)
    {
        _dapperDbContext = dapperDbContext ?? throw new ArgumentNullException(nameof(dapperDbContext));
    }
    public async Task<ApplicationUser?> AddUser(ApplicationUser user)
    {

        user.UserID = Guid.NewGuid();
        // Write Postgres SQL insert command to save user details. use coloum name same as AppilicationUser Class.
        // Use table Users
        using var connection = _dapperDbContext.CreateConnection();
        const string sql = "INSERT INTO \"public\".\"Users\" (\"UserID\", \"Email\", \"Password\", \"PersonName\", \"Gender\") VALUES(@UserID, @Email, @Password, @PersonName, @Gender);";
        
        try
        {
            if (await CheckIfUsersTableExists())
            {
                int rowCountAffected = await connection.ExecuteAsync(sql, user);

                if (rowCountAffected > 0)
                {
                    return user;
                }
            }
            return null;
        }
        catch (Exception ex)
        {
            // Log the exception or handle as needed
            // For now, just return null to indicate failure
            // Optionally, you could rethrow or wrap the exception
            return null;
        }
    }

    public async Task<ApplicationUser?> GetUserByEmailAndPassword(string? email, string? password)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            return null;

        using var connection = _dapperDbContext.CreateConnection();
        const string sql = @"
            SELECT 
                ""UserID"", ""Email"",""Password"",""PersonName"",""Gender""
            FROM ""public"".""Users""
            WHERE ""Email"" = @Email AND ""Password"" = @Password
            LIMIT 1;
        ";

        var user = await connection.QueryFirstOrDefaultAsync<ApplicationUser>(sql, new { Email = email, Password = password });
        return user;
    }

    public async Task<bool> CheckIfUsersTableExists()
    {
        using var connection = _dapperDbContext.CreateConnection();
        const string sql = @"
            SELECT * FROM information_schema.tables WHERE table_schema = 'public' AND table_name = 'Users';
        ";
        var result = await connection.QueryAsync(sql);
        return result.Any();
    }
}
