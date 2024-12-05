using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace social_network_api.DataObjects.Request.Common
{
    public class DeleteRequest
    {
        public int? id { get; set; }
        public int? reference_id { get; set; }
        public int? status_id { get; set; }
        public string? reason_denine { get; set; }
        public string? trans_code { get; set; }
        public List<int>? ids { get; set; }
    }
}
