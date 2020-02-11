using System.Data;
namespace TodoApi.DataAccess.Abstract.DBHelper
{
    public interface IDbConnectionFactory
    {
          IDbConnection CreateConnection();
    }
}