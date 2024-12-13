using social_network_api.DataObjects.Request.Authen;
using social_network_api.DataObjects.Request.Common;
using social_network_api.DataObjects.Request.Info;
using social_network_api.DataObjects.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace social_network_api.Interfaces.Info
{
    public interface IUser
    {
        public ApiResponse Register(RegisterRequest req);
        public ApiResponse Delete(DeleteRequest delReq);
        public ApiResponse Update(UserRequest req);
        public ApiResponse Get(string username);
        public ApiResponse UpdateBase(string username, UserRequest req);
        public ApiResponse ChangePassword(string username, ChangePasswordRequest req);
       

    }
}
