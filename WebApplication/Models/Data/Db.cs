using System.Configuration;
using System.Data.SqlClient;

namespace WebApplication.Models.Data
{
    public class Db
    {
        private readonly string _connectionString;

        public Db()
        {
            _connectionString = ConfigurationManager
                .ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}