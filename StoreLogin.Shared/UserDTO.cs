using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace StoreLogin.Shared
{
    public class UserDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("sessionId")]
        public string SessionId { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("gameCredit")]
        public int GameCredit { get; set; }
    }
}
