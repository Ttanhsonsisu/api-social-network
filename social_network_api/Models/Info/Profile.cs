
using social_network_api.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace social_network_api.Models.Info
{
    public class Profile : MasterCommonModel
    {
        public int Id { get; set; }
        public string? Avatar { get; set; }
        public string? CoverPhoto { get; set; }
        public string? Address { get; set; }
   
    }
}
