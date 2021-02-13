using StoreLogin.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheStoreLoginAPI.Data
{
    public interface IRepository
    {
        IQueryable<UserDTO> Users { get; }

        Task CreateUserAsync(UserDTO user);
    }
}
