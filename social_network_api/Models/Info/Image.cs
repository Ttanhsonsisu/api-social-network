
using social_network_api.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace social_network_api.Models.Info
{
    public class Image : MasterCommonModel
    {
        public int Id { get; set; }
        public string? title { get; set; }
        public string Url { get; set; }
        public string? Size_Img { get; set; } = String.Empty;
        public bool Is_Active { get; set; } = true;

    }
}
