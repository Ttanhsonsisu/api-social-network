using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace social_network_api.Models.Common
{
    public class Notification
    {
        public int Id { get; set; }
        public int  Id_User { get; set; }
        public string? Title { get; set; } = string.Empty;
        public string? Content { get; set; } = string.Empty;
        public int Type { get; set; } = 1;
        public bool Is_Notification_System { get; set; } = false;
        public string user_created { get; set; }
        public DateTime date_created { get; set; }
    }
}
