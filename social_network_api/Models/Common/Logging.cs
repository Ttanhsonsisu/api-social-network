using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace social_network_api.Models.Common
{
    public class Logging
    {
        public int Id { get; set; }
        public string User_type { get; set; }
        public string Functions { get; set; }
        public string Actions { get; set; }
        public string IP { get; set; }
        public string Content { get; set; }
        public string Result_Logging { get; set; }
        public Boolean Is_Login { get; set; }
        public Boolean Is_Call_Api { get; set; }
        public string User_Created { get; set; }
        public string? Api_Name { get; set; } = string.Empty;
        public DateTime Date_Created { get; set; }
    }
}
