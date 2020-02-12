using System.Threading.Tasks;
using TodoApi.DataAccess.Abstract.Account;
using TodoApi.Shared.Generics;
using TodoApi.DataAccess.Abstract.DBHelper;
using Dapper;
using System.Data;
using System;

namespace TodoApi.DataAccess.Repository.Account
{
    public class AuthRepository : IAuthRepository
    {
        #region Private Properties
        // private readonly IBaseRepository _baseRepository;
        private readonly IDbConnectionFactory _dbConnectionFactory;

        #endregion
        #region Constructor
        public AuthRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }
        #endregion
        public async Task<User> Login(string userName, string password)
        {
            var user = new User();
            using (var conn = _dbConnectionFactory.CreateConnection())
            {
                var parameter = new DynamicParameters();
                parameter.Add("@UserName", userName);
                user = await conn.QueryFirstOrDefaultAsync<User>("SP_GET_USER", parameter, commandType: CommandType.StoredProcedure);

            }
            if (user == null)
                return null;
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;
            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0, len = computedHash.Length; i < len; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                        return false;
                }

            }
            return true;
        }

        public async Task<string> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            using (var conn = _dbConnectionFactory.CreateConnection())
            {
                var parameter = new DynamicParameters();
                parameter.Add("@PasswordHash", user.PasswordHash);
                parameter.Add("@PasswordSalt", user.PasswordSalt);
                parameter.Add("@UserName", user.UserName);
                parameter.Add("@Result", dbType: DbType.String, direction: ParameterDirection.Output, size: 30);
                await conn.ExecuteAsync("SP_USER_REG", parameter, commandType: CommandType.StoredProcedure);
                return parameter.Get<string>("@Result"); ;
            }

        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public Task<bool> UserExists(string userName)
        {
            throw new System.NotImplementedException();
        }
    }
}