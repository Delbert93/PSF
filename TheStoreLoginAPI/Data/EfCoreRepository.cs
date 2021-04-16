using StoreLogin.Shared;
using System;
using System.Collections.Generic;
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
        public IQueryable<UserDTO> Users => context.Users;

        public async Task CreateUserAsync(UserDTO user)
        {
            try
            {
                context.Users.Add(user);
                await context.SaveChangesAsync();
            }
            catch(Exception e)
            {
            }
        }

        public IQueryable<Admin> Admins => context.Admins;

        public async Task CreateAdminAsync(Admin admin)
        {
            try
            {
                context.Admins.Add(admin);
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
            }
        }

        public async Task AssignSessionIdToUserAsync(int userid, Guid sessionId)
        {
            var user = await context.Users.FindAsync(userid);
            user.SessionId = sessionId.ToString();
            await context.SaveChangesAsync();

        }
    }
}
