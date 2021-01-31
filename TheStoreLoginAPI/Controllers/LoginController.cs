using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheStoreLoginAPI.Models;

namespace TheStoreLoginAPI.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task LoginValidation(string inputUsername, string inputPassword)
        {
            UserModel userModel = new UserModel();
            userModel.ValidateUsername(inputUsername);
            userModel.ValidatePassword(inputPassword);

            if(userModel.isValidUsername == true && userModel.isValidPassword == true)
            {
                //check those values against the db
            }
            else
            {
                //tell the UI "Invalid Username or Password"
            }
        }
    }
        
}
