using StoreLogin.Shared;
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

        public async Task<bool> ValidateRegistration(TaintedUserModel taintedUser)
        {
            var response = await client.PostAsJsonAsync("api/Registration/RegistrationValidation", taintedUser);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> ValidateCashout(TaintedUserModel taintedUser)
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

        public async Task<bool> AdminRegistration(TaintedUserModel taintedUser)
        {
            var response = await client.PostAsJsonAsync("admin/IsAdmin/AdminRegistration", taintedUser);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> AdminValidateLogin(TaintedUserModel taintedUser)
        {
            var response = await client.PostAsJsonAsync("admin/IsAdmin/LoginValidation", taintedUser);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<UserDTO>> GetUsersAsync()
        {
            var users = await client.GetFromJsonAsync<IEnumerable<UserDTO>>("api/Login");
            return users;
        }

        public async Task<Admin> GetAdminKey(String userName)
        {
            var users = await client.GetFromJsonAsync<IEnumerable<Admin>>("admin/IsAdmin");
            var key = users.Where(k => k.Username == userName).First();
            return key;
        }

        public async Task<Admin> CheckAdminKey(String key)
        {
            var users = await client.GetFromJsonAsync<IEnumerable<Admin>>("admin/IsAdmin");
            var userkey = users.Where(k => k.Key == key).First();
            return userkey;
        }
    }
}
