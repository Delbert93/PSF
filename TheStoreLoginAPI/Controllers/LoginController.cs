using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StoreLogin.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheStoreLoginAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        [HttpGet]
        public string Get() => "It's up.";

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
            string AdminUsername = "XYZxyzwwwzyxzyx";
            string AdminPassword = "abcxyzefgqrshijtuv";

            if (userModel.getUsername() == AdminUsername && userModel.getPassword() == AdminPassword)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }

}
