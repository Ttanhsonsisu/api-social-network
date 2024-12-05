
using social_network_api.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace social_network_api.Models.Post
{
    public class Post : MasterCommonModel
    {
        public int Id { get; set; }
        public int? Id_User { get; set; }
        public string? Content { get; set; } = String.Empty;
        public int? Id_Media { get; set; }
        public int Count_Like { get; set; } = 0;
        public int Count_Comment { get; set; } = 0;
        public int Count_Share { get; set; } = 0;
        public int? Status { get; set; } = 1;

    }
}
