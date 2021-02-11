using System;
using System.Collections.Generic;
using System.Text;

namespace StoreLogin.Shared
{
    public class UserDTO
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public int gameCredit { get; set; }
        public IEnumerable<UserDTO> users { get; set; }
    }
}
