using social_network_api.DataObjects.Request.Authen;
using social_network_api.DataObjects.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace social_network_api.Interfaces.Info
{
    public interface IProfile
    {
        public ApiResponse Create(string user, ProfileRequest req);
        public ApiResponse Update(string user, ProfileRequest req);
        public ApiResponse Delete(string user);
        public ApiResponse Get(string user);

    }
}
