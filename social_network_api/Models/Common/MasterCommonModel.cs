using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace social_network_api.Models.Common
{
    public class MasterCommonModel
    {
        public string? User_Updated { get; set; }
        public string? User_Created { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Updated { get; set; }
    }
}
