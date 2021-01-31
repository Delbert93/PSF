using Microsoft.AspNetCore.Mvc;
using StoreLogin.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheStoreLoginAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        [HttpPost("[action]")]
        public async Task LoginValidation(TaintedUserModel taintedUser)
        {
            //var url = "https://delbert93.github.io/PSF/";
            //if (Request.Headers.Contains(url))
            //{
            //    var values = Request.Headers.GetValues("Origin");
            //    // Do stuff with the values... probably .FirstOrDefault()
            //}
            UserModel userModel = new UserModel();
            userModel.ValidateUsername(taintedUser.GetUsername());
            userModel.ValidatePassword(taintedUser.GetPassword());

            if(userModel.isValidUsername == true && userModel.isValidPassword == true)
            {
                if(DBUserInfoLoginValidation(userModel))
                {
                    //tell UI "Login Successful;
                }
                else
                {
                    //tell the UI "Invalid Username or Password"
                }
            }
            else
            {
                //tell the UI "Invalid Username or Password"
            }
        }

        //in class validation until DB is set up
        public bool DBUserInfoLoginValidation(UserModel userModel)
        {
            string AdminUsername = "XYZxyzwwwzyxzyx";
            string AdminPassword = "abcxyzefgqrshijtuv";

            if(userModel.getUsername() == AdminUsername && userModel.getPassword() == AdminPassword)
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
