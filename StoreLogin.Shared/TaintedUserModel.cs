﻿using System;
using System.Collections.Generic;
using System.Text;

namespace StoreLogin.Shared
{
    public class TaintedUserModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int Credits { get; set; }

    }
}
