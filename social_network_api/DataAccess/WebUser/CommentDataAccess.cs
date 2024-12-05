using social_network_api.Data;
using social_network_api.DataObjects.CommentRequest;
using social_network_api.DataObjects.Request.Common;
using social_network_api.DataObjects.Response;
using social_network_api.Interfaces.Comment;
using social_network_api.Models.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace social_network_api.DataAccess.WebUser
{
    public class CommentDataAccess : IComment
    {
        private readonly ApplicationDBContext _context;

        public CommentDataAccess( ApplicationDBContext context)
        {
            this._context = context;
        }

        public ApiResponse Create(int idUser, CommentRequest commentRequest)
        {
            if (idUser == null)
            {
                return new ApiResponse("ERROR_USER_MISING");
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                var data = new Comment();
                data.Content = commentRequest.Content;
                data.Count_Command = 0;
                data.Count_Like = 0;
                data.Id_Parent = commentRequest.Id_Parent;
                data.Id_Post = commentRequest.Id_Post;
                data.Id_User = commentRequest.Id_User;
                data.Status = commentRequest.Status;

                _context.Commands.Add(data);
                _context.SaveChanges();

                transaction.Commit();
                transaction.Dispose();
            }
            throw new NotImplementedException();
        }

        public ApiResponse Delete(int idUser, DeleteRequest id)
        {
            if (idUser == null)
            {
                return new ApiResponse("ERROR_USER_MISSING");
            }

            if (id == null)
            {
                return new ApiResponse("ERROR_COMMENT_ID_MISSING");
            }


            throw new NotImplementedException();
        }

        public ApiResponse GetChil(int parentComment)
        {
            throw new NotImplementedException();
        }

        public ApiResponse GetDetail(int idUser, int id)
        {
            throw new NotImplementedException();
        }

        public ApiResponse GetList(int idUser)
        {
            throw new NotImplementedException();
        }

        public ApiResponse Update(int idUse, CommentRequest postRequest)
        {
            throw new NotImplementedException();
        }
    }
}
