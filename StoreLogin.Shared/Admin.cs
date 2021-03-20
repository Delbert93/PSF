using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace StoreLogin.Shared
{
    public class Admin
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

    }
}