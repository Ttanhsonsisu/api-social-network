using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace social_network_api.Models.Command
{
    public class CommentLike
    {
        public int Id { get; set; }
        public int Id_User { get; set; }
        public int Id_Post { get; set; }
        public int Id_Command { get; set; }
    }
}
