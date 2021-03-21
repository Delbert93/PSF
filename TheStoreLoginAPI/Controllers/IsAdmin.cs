using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreLogin.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                Random rnd = new Random();
                int randKey = rnd.Next(1000000, 2000000000);
                admin.Key = userModel.HashKey(randKey.ToString());
                await repository.CreateAdminAsync(admin);
                return Ok();
            }
            else
            {
                //tell the UI "Invalid Username or Password"
                return BadRequest();
            }
        }

        [HttpPost("[action]")]
        public IActionResult LoginValidation(TaintedUserModel taintedUser)
        {
            UserModel userModel = new UserBuilder()
                    .UseName(taintedUser.Username)
                    .UsePassword(taintedUser.Password)
                    .Build();

            if (userModel.isValidUser == true)
            {

                if (DBUserInfoLoginValidation(userModel))
                {
                    //tell UI "Login Successful;
                    UserSnapshot userSnapshot = new UserSnapshot(userModel.getUsername()) { gameCredit = userModel.getGameCredit() };

                    return Ok();
                }
                else
                {
                    //tell the UI "Invalid Username or Password"
                    return BadRequest();
                }
            }
            else
            {
                //tell the UI "Invalid Username or Password"
                return BadRequest();
            }
        }

        //in class validation until DB is set up
        public bool DBUserInfoLoginValidation(UserModel userModel)
        {
            //string AdminUsername = "XYZxyzwwwzyxzyx";
            //string AdminPassword = "abcxyzefgqrshijtuv";
            string userName = userModel.getUsername();
            string userPassword = userModel.getPassword();

            var response = Get();
            bool foundUser = false;
            foreach (var r in response.Result)
            {
                if (r.Username == userName && r.Password == userPassword)
                {
                    foundUser = true;
                }
            }

            return foundUser;

        }
    }
}
