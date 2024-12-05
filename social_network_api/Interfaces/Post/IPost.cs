using social_network_api.DataObjects.ActionObject.Post;
using social_network_api.DataObjects.Request.Common;

using social_network_api.DataObjects.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace social_network_api.Interfaces.Post
{
    public interface IPost
    {
        public ApiResponse GetList(int idUser, PostRequest postRequest);
        public ApiResponse GetDetail(int idUser, PostRequest postRequest);
        public ApiResponse Create(int idUser, PostRequest postRequest);
        public ApiResponse Update(int idUser, PostRequest postRequest);
        public ApiResponse Delete(int idUser, DeleteRequest id);
    }
}
