using Microsoft.AspNetCore.Mvc;
using System;
using StoreLogin.Shared;
using Microsoft.EntityFrameworkCore;
using TheStoreLoginAPI.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog;

namespace TheStoreLoginAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : Controller
    {
        private readonly IRepository repository;

        public RegistrationController(IRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet()]
        public async Task<IEnumerable<UserDTO>> Get() => await repository.Users.ToListAsync();

        [HttpPost("[action]")]
        public async Task<IActionResult> RegistrationValidation(TaintedUserModel taintedUser)
        {
            UserModel userModel = new UserBuilder()
                .UseName(taintedUser.Username)
                .UsePassword(taintedUser.Password)
                .UseEmail(taintedUser.Email)
                .Build();

            if (userModel.isValidUser == true)
            {
                //tell UI "Login Successful;
                UserDTO userDTO = new UserDTO();
                userDTO.Username = userModel.getUsername();
                //userDTO.Password = userModel.HashPassword(userModel.getPassword());
                //This is causing the password to get hashed twice. Did we want this or was it something we didnt relize was going on. Removed it for now so I dont have to hash the login twice. 
                userDTO.Password = userModel.getPassword();
                userDTO.Email = userModel.getEmail();
                userDTO.GameCredit = userModel.getGameCredit();
                await repository.CreateUserAsync(userDTO);
                Log.Information("User " + userModel.getUsername() + " Has been created successfully");
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
