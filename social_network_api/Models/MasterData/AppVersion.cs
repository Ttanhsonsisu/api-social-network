using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace social_network_api.Models.MasterData
{
    public class AppVersion
    {
        public int Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? Version_Name { get; set; } = string.Empty;
        public int? Build { get; set; } 
        public int? Platform { get; set; }
        public bool?  Is_Active { get; set; }
        public bool? is_require_update { get; set; }
        public DateTime? Apply_Date { get; set; }
        public DateTime? Create_At { get; set; }
        public DateTime? Update_At { get; set; }
    }
}
