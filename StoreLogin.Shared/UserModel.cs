﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace StoreLogin.Shared
{
    public class UserModel
    {
        private string username;
        private readonly string password;
        private string email;
        private int gameCredit;
        private bool isValidPassword;
        private bool isValidUsername;
        private bool isValidEmail;
        public bool isValidUser;
        public bool readFlag = false;

        public UserModel(string _username, string _password)
        {
            isValidUser = false;
            isValidUsername = false;
            isValidPassword = false;
            isValidEmail = true;

            ValidateUsername(_username);
            ValidatePassword(_password);
            ValidateUser();

            if(isValidUser == true)
            {
                password = HashPassword(_password);
                username = _username;
            }
        }
        public UserModel(string _username, string _password, string _email)
        {
            isValidUser = false;
            isValidUsername = false;
            isValidPassword = false;
            isValidEmail = false;

            ValidateUsername(_username);
            ValidatePassword(_password);
            ValidateEmail(_email);
            gameCredit = 1500;
            ValidateUser();

            if (isValidUser == true)
            {
                password = HashPassword(_password);
                username = _username;
                email = _email;
            }
        }
        public void ValidatePassword(string _password)
        {

            Regex objAlphaPattern = new Regex(@"^[a-zA-Z0-9]*$");
            var hasMinimum12Chars = new Regex(@".{12,}");

            var isValidated = objAlphaPattern.IsMatch(_password) && hasMinimum12Chars.IsMatch(_password) && String.IsNullOrEmpty(_password) != true;
            if (isValidated)
            {
                isValidPassword = true;
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
            var isValidated = objAlphaPattern.IsMatch(_username) && hasMinimum8Chars.IsMatch(_username) && String.IsNullOrEmpty(_username) != true;

            if (isValidated)
            {
                isValidUsername = true;
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

        public string getEmail()
        {
            return this.email;
        }

        public int getGameCredit()
        {
            return this.gameCredit;
        }

        public string HashPassword(string password)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(password);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            String hash = System.Text.Encoding.ASCII.GetString(data);
            return hash;
        }

        public string HashKey(string key)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(key);
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
            }
            catch (FormatException)
            {
                isValidEmail = false;
            }

        }

        public void ValidateUser()
        {
            if(isValidUsername == true && isValidPassword == true && isValidEmail == true)
            {
                isValidUser = true;
            }
            else
            {
                Result.Fail("Invalid Username or Password");
                isValidUser = false;
            }
        }
    }
}
