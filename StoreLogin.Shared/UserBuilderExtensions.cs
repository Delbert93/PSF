using System;
using System.Collections.Generic;
using System.Text;

namespace StoreLogin.Shared
{
    public static class UserBuilderExtensions
    {
        public static UserBuilder UseName(this UserBuilder ub, string username)
        {
                ub.Username = username;
                return ub;
        }

        public static UserBuilder UsePassword(this UserBuilder ub, string password)
        {
            ub.Password = password;
            return ub;
        }

        public static UserBuilder UseEmail(this UserBuilder ub, string email)
        {
            ub.Email = email;
            return ub;
        }

        public static UserModel Build(this UserBuilder ub)
        {
            UserModel userModel = new UserModel(ub.Username, ub.Password, ub.Email);
            return userModel;
        }
    }
}
