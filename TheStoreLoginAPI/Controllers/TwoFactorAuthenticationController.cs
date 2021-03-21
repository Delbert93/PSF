using Google.Authenticator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreLogin.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheStoreLoginAPI.Data;

namespace TheStoreLoginAPI.Controllers
{
    public class TwoFactorAuthenticationController : Controller
    {
        private readonly IRepository repository;

        public TwoFactorAuthenticationController(IRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        public async Task<IEnumerable<UserDTO>> Get() => await repository.Users.ToListAsync();

        [HttpGet]
        public ActionResult Enable()
        {
            UserDTO user = new UserDTO();
            //UserDTO user = // TODO: fetch signed in user from a database
            TwoFactorAuthenticator twoFactor = new TwoFactorAuthenticator();
            var setupInfo =
                twoFactor.GenerateSetupCode("myapp", user.Email, TwoFactorKey(user), false, 3);
            ViewBag.SetupCode = setupInfo.ManualEntryKey;
            ViewBag.BarcodeImageUrl = setupInfo.QrCodeSetupImageUrl;
            return View();
        }

        private static string TwoFactorKey(UserDTO user)
        {
            return $"myverysecretkey+{user.Email}";
        }
    }
}
