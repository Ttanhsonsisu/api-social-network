using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace social_network_api.DataObjects.Response
{
    public class DataListRespone
    {
        public int Total_elements { get; set; }
        public int Total_page { get; set; }
        public int Page_no { get; set; }
        public int Page_Size { get; set; }
        public object Data { get; set; }
        public decimal? Data_Count { get; set; }
        public decimal? Values { get; set; }
        public decimal? Data_Quantity { get; set; }
    }
}
