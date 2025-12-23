using Microsoft.Data.SqlClient;

namespace WebApplication.Models.Data
{
    /// <summary>
    /// Database connection helper for .NET Core
    /// </summary>
    public class Db
    {
        private readonly string _connectionString;

        public Db(IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(configuration);
            
            _connectionString = configuration.GetConnectionString("DefaultConnection") 
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        /// <summary>
        /// Gets a new SQL connection instance
        /// </summary>
        /// <returns>SqlConnection instance</returns>
        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        /// <summary>
        /// Gets the connection string value
        /// </summary>
        /// <returns>Connection string</returns>
        public string GetConnectionString() => _connectionString;
    }
}