using social_network_api.DataObjects.Request.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace social_network_api.DataObjects.Request.Authen
{
    public class FriendRequest : PaggingRequest
    {
        public int? id { get; set; }
        public string? name { get; set; }
        public int? status { get; set; }
        public string? description { get; set; }
        public Boolean? is_default { get; set; }
    }
}
