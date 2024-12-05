using social_network_api.DataObjects.CommentRequest;
using social_network_api.DataObjects.Request.Common;
using social_network_api.DataObjects.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace social_network_api.Interfaces.Comment
{
    public interface IComment
    {
        public ApiResponse GetList(int idUser);
        public ApiResponse GetChil(int parentComment);
        public ApiResponse GetDetail(int idUser, int id);
        public ApiResponse Create(int idUser, CommentRequest postRequest);
        public ApiResponse Update(int idUse, CommentRequest postRequest);
        public ApiResponse Delete(int idUse, DeleteRequest id);
    }
}
