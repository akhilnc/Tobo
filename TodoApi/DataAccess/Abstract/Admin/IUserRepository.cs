using System.Threading.Tasks;
namespace TodoApi.DataAccess.Abstract.Admin
{
    public interface IUserRepository
    {
        Task<string> getUser();
    }
}