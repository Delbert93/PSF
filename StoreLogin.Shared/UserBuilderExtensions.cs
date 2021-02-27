using System;
using System.Collections.Generic;
using System.Text;

namespace StoreLogin.Shared
{
    public static class UserBuilderExtensions
    {
        public static UserBuilder UseName(this UserBuilder ub, string username)
        {
            if (System.Text.ASCIIEncoding.Unicode.GetByteCount(username) < 1000)
            {
                ub.Username = username;
                return ub;
            }
            else
            {
                ub.Username = "a";
                return ub;
            }
        }

        public static UserBuilder UsePassword(this UserBuilder ub, string password)
        {
            if (System.Text.ASCIIEncoding.Unicode.GetByteCount(password) < 1000)
            {
                ub.Password = password;
                return ub;
            }
            else
            {
                ub.Password = "a";
                return ub;
            }
        }

        public static UserBuilder UseEmail(this UserBuilder ub, string email)
        {
            if (System.Text.ASCIIEncoding.Unicode.GetByteCount(email) < 1000)
            {
                ub.Email = email;
                return ub;
            }
            else
            {
                ub.Email = "a";
                return ub;
            }
        }
        public static UserModel Build(this UserBuilder ub)
        {
            if (ub.Email == null)
            {
                UserModel userModel = new UserModel(ub.Username, ub.Password);
                return userModel;
            }
            else
            {
                UserModel userModel = new UserModel(ub.Username, ub.Password, ub.Email);
                return userModel;
            }
        }
    }
}
