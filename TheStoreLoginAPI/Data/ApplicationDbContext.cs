using Microsoft.EntityFrameworkCore;
using StoreLogin.Shared;

namespace TheStoreLoginAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<UserModel> Users { get; set; }
    }
}
