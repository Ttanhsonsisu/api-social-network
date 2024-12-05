using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace social_network_api.DataObjects.Request.Info
{
    public class UserRequest
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int? Role { get; set; }
        public string? Full_Name { get; set; }
        public int?  Id_Profile { get; set; }
    }
}
