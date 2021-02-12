using System;
using System.Collections.Generic;
using System.Text;

namespace StoreLogin.Shared
{
    public class UserSnapshot
    {
        private readonly string username;
        public int gameCredit { get; set; }

        public UserSnapshot(string _username)
        {
            username = _username;
        }
    }
}
