using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace social_network_api.Models.Authen
{
    public class UserPermission
    {
        public int Id { get; set; }
        public int User_Id { get; set; }
        public int Action_Id { get; set; }
    }
}
