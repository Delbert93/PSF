﻿using StoreLogin.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace TheStoreLoginFrontEnd
{
    public class PublicApiService
    {
        private readonly HttpClient client;

        public PublicApiService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<bool> ValidateLogin(TaintedUserModel taintedUser)
        {
            var response = await client.PostAsJsonAsync("api/Login/LoginValidation", taintedUser);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
