using System;
using System.Collections.Generic;
using System.Text;

namespace StoreLogin.Shared
{
    public class TaintedUserModel
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public string GetUsername()
        {
            return this.Username;
        }
        public string GetPassword()
        {
            return this.Password;
        }
    }
}
