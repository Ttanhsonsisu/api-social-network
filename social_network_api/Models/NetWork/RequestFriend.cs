using social_network_api.Models.Common;

namespace social_network_api.Models.NetWork
{
    public class RequestFriend : MasterCommonModel
    {
        public int Id { get; set; }
        public int User_Id_From { get; set; }
        public int User_Id_To { get; set; }
        public int? Status { get; set; }

    }
}
