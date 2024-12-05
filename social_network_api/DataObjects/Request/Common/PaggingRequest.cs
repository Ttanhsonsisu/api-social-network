using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace social_network_api.DataObjects.Request.Common
{
    public class PaggingRequest
    {
        public int page_size { get; set; }
        public int page_no { get; set; }
    }
}
