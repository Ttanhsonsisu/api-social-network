using social_network_api.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace social_network_api.Models.Info
{
    public class Education : MasterCommonModel
    {
        public int Id { get; set; }
        public int Id_User { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime? EstablishedYear { get; set; }
        public string? Description { get; set; }
    }
}
