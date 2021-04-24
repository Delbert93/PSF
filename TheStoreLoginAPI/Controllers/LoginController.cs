using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StoreLogin.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheStoreLoginAPI.Data;
using Serilog;

namespace TheStoreLoginAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly IRepository repository;

        public LoginController(IRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        public async Task<IEnumerable<UserDTO>> Get() => await repository.Users.ToListAsync();

        [HttpPost("[action]")]
        public async Task<IActionResult> LoginValidation(TaintedUserModel taintedUser)
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
                    
                    HttpContext.Session.SetString("sessionId", Guid.NewGuid().ToString());
                    
                   
                    //tell UI "Login Successful;
                    UserDTO userDTO = new UserDTO();
                    userDTO.Username = userModel.getUsername();
                    //userDTO.Password = userModel.HashPassword(userModel.getPassword());
                    //This is causing the password to get hashed twice. Did we want this or was it something we didnt relize was going on. Removed it for now so I dont have to hash the login twice. 
                    userDTO.Password = userModel.getPassword();
                    userDTO.Email = userModel.getEmail();
                    userDTO.GameCredit = userModel.getGameCredit();

                    var sessionString = Guid.NewGuid().ToString();
                    await repository.AssignSessionIdToUserAsync(userDTO.Id, sessionString);

                    Log.Information("User " + userModel.getUsername() + " Has logged in successfully");
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
                    foundUser= true;
                }
            }

            return foundUser;
        }

    }

}
