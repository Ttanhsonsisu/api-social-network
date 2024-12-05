using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace social_network_api.DataObjects.CommentRequest
{
    public class CommentRequest
    {
        public int Id { get; set; }
        public int Id_Post { get; set; }
        public int Id_User { get; set; }
        public int? Id_Parent { get; set; }
        public string? Content { get; set; }
        public int Count_Like { get; set; } 
        public int Count_Command { get; set; } 
        public int Status { get; set; } 
    }
}