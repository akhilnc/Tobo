using System.Data;
using System.Data.SqlClient;
using TodoApi.DataAccess.Abstract.DBHelper;
namespace TodoApi.DataAccess.Repository.DBHelper
{
    public class SqlConnectionFactory:IDbConnectionFactory
    {
        private readonly string _connectionString;

    public SqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
    {
        var conn = new SqlConnection(_connectionString);
        conn.Open();
        return conn;
    }
    }
}