using StoreLogin.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminAPI.Data
{
    public class EfCoreRepository : IRepository
    {
        private readonly ApplicationDbContext context;
        public EfCoreRepository(ApplicationDbContext context)
        {
            this.context = context ?? throw new ArgumentException(nameof(context));
        }
        public IQueryable<Admin> Admins => context.Admins;
    }
}
