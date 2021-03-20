using StoreLogin.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminAPI.Data
{
    public interface IRepository
    {
        IQueryable<Admin> Admins { get; }
    }
}
