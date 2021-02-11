using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreLogin.Shared;
using Microsoft.EntityFrameworkCore;

namespace TheStoreLoginAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : Controller
    {
        [HttpGet]
        public string Get() => "It's up also.";

        [HttpPost("[action]")]
        public IActionResult RegistrationValidation(TaintedUserModel taintedUser)
        {
            UserModel userModel = new UserModel(taintedUser.Username, taintedUser.Password, taintedUser.Email);

            if (userModel.isValidUsername == true && userModel.isValidPassword == true && userModel.isValidEmail)
            {
                if (DBUserInfoLoginValidation(userModel))
                {
                    //tell UI "Login Successful;
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
