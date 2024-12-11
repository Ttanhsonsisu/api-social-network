using social_network_api.DataObjects.Request.Authen;
using social_network_api.DataObjects.Request.Common;
using social_network_api.DataObjects.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace social_network_api.Interfaces.AddFriend
{
    public interface IFriend
    {
        public ApiResponse GetList(int idUser, FriendRequest req);
        public ApiResponse AddFriend(int idUser, FriendRequest req, string username);
        public ApiResponse DeleteFriend(int idUser, DeleteRequest req);
        public ApiResponse UpdateStatus(int idUser, FriendRequest req);
        public ApiResponse ChangeStatusFriend(string username, FriendRequest request);
        public ApiResponse GetCountFriend(string username);
    }
}
