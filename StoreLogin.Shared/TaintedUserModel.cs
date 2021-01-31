using System;
using System.Collections.Generic;
using System.Text;

namespace StoreLogin.Shared
{
    public class TaintedUserModel
    {
        public string username;
        public string password;

        public string GetUsername()
        {
            return this.username;
        }
        public string GetPassword()
        {
            return this.password;
        }
    }
}
