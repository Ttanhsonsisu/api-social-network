using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace social_network_api.Models.Authen
{
    public class UserGroupPermission
    {
        public int Id { get; set; }
        public int User_Group_Id { get; set; }
        public int Action_Id { get; set; }
    }
}
