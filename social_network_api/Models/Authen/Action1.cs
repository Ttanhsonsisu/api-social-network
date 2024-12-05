using social_network_api.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace social_network_api.Models.Authen
{
    public class Action1 : MasterCommonModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string? description { get; set; }
        public bool Is_Default { get; set; } = true;
        public int Status { get; set; } = 1;
        public string? Url { get; set; }
    }
}
