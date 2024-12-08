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
        public ApiResponse GetListTree(CommentRequest comemntRequest);
        public ApiResponse GetListParent(CommentRequest comemntRequest);
        public ApiResponse GetChil(CommentRequest comemntRequest);
        public ApiResponse GetDetail(int idUser, int? id);
        public ApiResponse Create(int idUser, CommentRequest comemntRequest);
        public ApiResponse Update(int idUser, CommentRequest commentRequest);
        public ApiResponse Delete(int idUse, DeleteRequest id);
    }
}
