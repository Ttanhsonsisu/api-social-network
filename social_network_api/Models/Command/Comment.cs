using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace social_network_api.Models.Command
{
    public class Comment
    {
        public int Id { get; set; }
        public int Id_Post { get; set; }
        public int Id_User { get; set; }
        public int? Id_Parent { get; set; }
        public string? Content { get; set; } = string.Empty;
        public int Count_Like { get; set; } = 0;
        public int Count_Command { get; set; } = 0;
        public int Status { get; set; } = 1;
    }
}
