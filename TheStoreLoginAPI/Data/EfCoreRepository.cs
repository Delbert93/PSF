using StoreLogin.Shared;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace TheStoreLoginAPI.Data
{
    public class EfCoreRepository : IRepository
    {
        private readonly ApplicationDbContext context;
        public EfCoreRepository(ApplicationDbContext context)
        {
            this.context = context ?? throw new ArgumentException(nameof(context));
        }

        public IQueryable<UserModel> Users => context.Users;

        public async Task CreateUserAsync(UserModel user)
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
        }
    }
}
