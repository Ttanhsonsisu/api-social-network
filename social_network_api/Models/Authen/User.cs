using social_network_api.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace social_network_api.Models.Authen
{
    public class User : MasterCommonModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string? Full_Name { get; set; }
        public int? Id_Profile { get; set; }
        public string Password { get; set; }
        public int Status { get; set; } = 1;
        public bool Is_Sysadmin { get; set; } = false;
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public int Role { get; set; } = 1;
        public string? Address { get; set; }
        public int? User_Group_Id { get; set; }
    }
}
