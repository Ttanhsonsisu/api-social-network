
using social_network_api.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace social_network_api.Models.MasterData
{
    public class Media : MasterCommonModel
    {
        public int Id { get; set; }
        public bool Is_Video { get; set; } = false;
        public bool Is_Img { get; set; } = false;
        public int Use_Upload { get; set; } = 0;
        public int Status { get; set; } = 1;
    }
}
