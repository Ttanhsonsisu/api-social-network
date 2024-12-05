using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace social_network_api.DataObjects.Request.Authen
{
    public class RegisterRequest
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int? Role { get; set; }
        public string? Full_Name { get; set; }

    }
}
