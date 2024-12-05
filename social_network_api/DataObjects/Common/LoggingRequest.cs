using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace social_network_api.DataObjects.Common
{
    public class LoggingRequest
    {
        public string User_type { get; set; }
        public string? Functions { get; set; }
        public string? Actions { get; set; }
        public string? IP { get; set; }
        public string? Content { get; set; }
        public string? Result_Logging { get; set; }
        public bool Is_Login { get; set; }
        public bool Is_Call_Api { get; set; }
        public string User_Created { get; set; }
        public DateTime Date_Created { get; set; }
        public string? Api_Name { get; set; }
    }
}
