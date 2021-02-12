using Microsoft.AspNetCore.Mvc;
using System;
using StoreLogin.Shared;
using Microsoft.EntityFrameworkCore;
using TheStoreLoginAPI.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public IActionResult RegistrationValidation(TaintedUserModel taintedUser)
        {
            UserModel userModel = new UserBuilder()
                .UseName(taintedUser.Username)
                .UsePassword(taintedUser.Password)
                .UseEmail(taintedUser.Email)
                .BuildWithEmail();

            if (userModel.isValidUser == true)
            {
                //tell UI "Login Successful;
                UserDTO userDTO = new UserDTO();
                userDTO.username = userModel.getUsername();
                userDTO.password = userModel.HashPassword(userModel.getPassword());
                userDTO.email = userModel.getEmail();
                userDTO.gameCredit = userModel.getGameCredit();
                repository.CreateUserAsync(userDTO);
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
