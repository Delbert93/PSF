using StoreLogin.Shared;
using System.Linq;
using System.Threading.Tasks;

namespace TheStoreLoginAPI.Data
{
    public interface IRepository
    {
        IQueryable<UserModel> Users { get; }

        Task CreateUserAsync(UserModel user);
    }
}
