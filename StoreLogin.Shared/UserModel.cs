using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace StoreLogin.Shared
{
    public class UserModel
    {
       

        //strings are automatically immutable, but c#, under the hood, makes it seem like you can change it, when it's really just creating a new string
        private string username;
        private string password;
        private string email;
        private int gameCredit;
        public bool readFlag = false;
        public bool isValidPassword;
        public bool isValidUsername;
        public bool isValidEmail;

        public UserModel(string _username, string _password)
        {
            ValidateUsername(_username);
            ValidatePassword(_password);
        }
        public UserModel(string _username, string _password, string _email)
        {
            ValidateUsername(_username);
            ValidatePassword(_password);
            ValidateEmail(_email);
            gameCredit = 1500;
        }

        public void ValidatePassword(string _password)
        {

            Regex objAlphaPattern = new Regex(@"^[a-zA-Z0-9]*$");
            var hasMinimum12Chars = new Regex(@".{12,}");

            var isValidated = objAlphaPattern.IsMatch(_password) && hasMinimum12Chars.IsMatch(_password);
            if (isValidated)
            {
                isValidPassword = true;
                this.password = HashPassword(_password);
            }
            else
            {
                isValidPassword = false;
            }
        }

        public void ValidateUsername(string _username)
        {

            Regex objAlphaPattern = new Regex(@"^[a-zA-Z0-9]*$");
            var hasMinimum8Chars = new Regex(@".{8,}");
            var isValidated = objAlphaPattern.IsMatch(_username) && hasMinimum8Chars.IsMatch(_username);

            if (isValidated)
            {
                isValidUsername = true;
                this.username = _username;
            }
            else
            {
                isValidUsername = false;
            }
        }

        public string getUsername()
        {
            return this.username;
        }
        public string getPassword()
        {
            if (readFlag == false)
            {
                readFlag = true;
                return this.password;
            }
            else
            {
                Trace.WriteLine("Cannot read this object more than once");
                return null;
            }
        }

        public string HashPassword(string password)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(password);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            String hash = System.Text.Encoding.ASCII.GetString(data);
            return hash;
        }

        public bool ValidateCredits()
        {
            return true;
        }

        public void ValidateEmail(string _email)
        {
            try
            {
                MailAddress m = new MailAddress(_email);

                isValidEmail = true;
                this.email = _email;
            }
            catch (FormatException)
            {
                isValidEmail = false;
            }

        }
    }
}
