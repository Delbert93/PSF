using Microsoft.EntityFrameworkCore;
using StoreLogin.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheStoreLoginAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<UserDTO> Users { get; set; }
    }
}
