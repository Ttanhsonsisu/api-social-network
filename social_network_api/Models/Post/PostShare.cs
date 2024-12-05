
using social_network_api.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace social_network_api.Models.Post
{
    public class PostShare : MasterCommonModel
    {
        public int Id { get; set; }
        public int Id_User { get; set; }
        public int Id_Post { get; set; }
    }
}
