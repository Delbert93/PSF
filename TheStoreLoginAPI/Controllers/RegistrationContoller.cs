using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreLogin.Shared;

namespace TheStoreLoginAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationContoller : Controller
    {
        [HttpPost("[action]")]
        public IActionResult RegistrationValidation(TaintedUserModel taintedUser)
        {
            UserModel userModel = new UserModel();
            userModel.ValidateUsername(taintedUser.Username);
            userModel.ValidatePassword(taintedUser.Password);

            if (userModel.isValidUsername == true && userModel.isValidPassword == true)
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
