using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace social_network_api.Models.NetWork
{
    public class FriendShip
    {
        public int Id { get; set; }
        public int Id_User { get; set; }
        public int? Id_Friend { get; set; }
        public int? Status { get; set; }
        public string? User_Created { get; set; }
        public DateTime? Date_created { get; set; }
    }
}
