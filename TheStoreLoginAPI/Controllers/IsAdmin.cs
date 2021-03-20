using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreLogin.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheStoreLoginAPI.Data;

namespace AdminAPI.Controllers
{
    [ApiController]
    [Route("admin/[controller]")]
    public class IsAdmin : Controller
    {
        private readonly IRepository repository;

        public IsAdmin(IRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        public async Task<IEnumerable<Admin>> Get() => await repository.Admins.ToListAsync();

        [HttpPost("[action]")]
        public async Task<IActionResult> AdminRegistration(TaintedUserModel taintedUser)
        {
            UserModel userModel = new UserBuilder()
                .UseName(taintedUser.Username)
                .UsePassword(taintedUser.Password)
                .UseEmail(taintedUser.Email)
                .Build();

            if (userModel.isValidUser == true)
            {
                //tell UI "Login Successful;
                Admin admin = new Admin();
                admin.Username = userModel.getUsername();
                admin.Password = userModel.getPassword();
                admin.Email = userModel.getEmail();
                await repository.CreateAdminAsync(admin);
                return Ok();
            }
            else
            {
                //tell the UI "Invalid Username or Password"
                return BadRequest();
            }
        }
    }
}
