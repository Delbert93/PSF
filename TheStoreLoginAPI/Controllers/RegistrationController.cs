using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreLogin.Shared;
using Microsoft.EntityFrameworkCore;
using TheStoreLoginAPI.Data;

namespace TheStoreLoginAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : Controller
    {
        private readonly IRepository repository;

        public RegistrationController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public string Get() => "It's up also.";

        [HttpPost("[action]")]
        public async Task<IActionResult> RegistrationValidation(TaintedUserModel taintedUser)
        {
            UserModel userModel = new UserModel(taintedUser.Username, taintedUser.Password, taintedUser.Email);

            if (userModel.isValidUser == true)
            {
                //tell UI "Login Successful;
                UserDTO userDTO = new UserDTO();
                userDTO.username = userModel.getUsername();
                userDTO.password = userModel.HashPassword(userModel.getPassword());
                userDTO.email = userModel.getEmail();
                userDTO.gameCredit = userModel.getGameCredit();
                await repository.CreateUserAsync(userDTO);
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
