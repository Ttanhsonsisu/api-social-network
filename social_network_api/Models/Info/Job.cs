
using social_network_api.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace social_network_api.Models.Info
{
    public class Job : MasterCommonModel
    {
        public int Id { get; set; }
        public int Id_User { get; set; }
        public string? Company { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }

    }
}
