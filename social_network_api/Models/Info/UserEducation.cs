
using social_network_api.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace social_network_api.Models.Info
{
    public class UserEducation : MasterCommonModel
    {
        public int Id { get; set; }
        public int Id_User { get; set; }
        public string Degree { get; set; }
        public DateTime? From_Year { get; set; }
        public DateTime? To_Year { get; set; }
        public bool Is_Graduate { get; set; } = false;
        public int Status { get; set; } = 1;

    }
}
