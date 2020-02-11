using System.Threading.Tasks;
using TodoApi.Shared.Generics;
namespace TodoApi.DataAccess.Abstract.Account
{
    public interface IAuthRepository
    {
         Task<string>Register(User user,string password);
         Task<User>Login(string userName,string password);
         Task<bool>UserExists(string userName);
    }
}