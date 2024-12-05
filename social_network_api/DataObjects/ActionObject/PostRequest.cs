using social_network_api.DataObjects.Request.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace social_network_api.DataObjects.ActionObject.Post
{
    public class PostRequest : PaggingRequest
    {
        public int? Id { get; set; }
        public string? Content { get; set; }
        public int? Id_Media { get; set; }
        public int? Status { get; set; } 
    }
}
