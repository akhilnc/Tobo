using System.Threading.Tasks;
using TodoApi.DataAccess.Abstract.Admin;
using Dapper;
using TodoApi.DataAccess.Abstract.DBHelper;
namespace TodoApi.DataAccess.Repository.Admin
{
    public class UserRepository : IUserRepository
    {
        #region Private Properties
        // private readonly IBaseRepository _baseRepository;
        private readonly IDbConnectionFactory _dbConnectionFactory;

        #endregion
        #region Constructor
        public UserRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }
        #endregion
        #region public methods
        public async Task<string> getUser()
        {
            using (var conn = _dbConnectionFactory.CreateConnection())
            {
                string sQuery = "SELECT top 1 Userid FROM mstLogin";
                var result = await conn.QueryFirstOrDefaultAsync<string>(sQuery);
                return result;
            }
        }
        #endregion
    }
}