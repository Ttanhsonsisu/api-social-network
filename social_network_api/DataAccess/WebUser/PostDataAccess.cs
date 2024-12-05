using social_network_api.Data;
using social_network_api.DataObjects.ActionObject.Post;
using social_network_api.DataObjects.Request.Common;

using social_network_api.DataObjects.Response;
using social_network_api.Extensions;
using social_network_api.Helpers;
using social_network_api.Interfaces.Post;
using social_network_api.Models.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace social_network_api.DataAccess.WebUser
{
    public class PostDataAccess : IPost
    {
        private readonly ApplicationDBContext _context;
        private readonly ICommonFunctions _commonFuntions;

        public PostDataAccess (ApplicationDBContext context, ICommonFunctions commonFuntions)
        {
            this._commonFuntions = commonFuntions;
            this._context = context;
        }

        public ApiResponse Create(int idUser, PostRequest postRequest)
        {
            if(idUser == null)
            {
                return new ApiResponse("NO_USER");
            }

            using(var transaction = _context.Database.BeginTransaction())
            {
                var data = new Post();
                data.Content = postRequest.Content;
                data.Date_Created = DateTime.Now;
                data.Id_Media = postRequest.Id_Media;
                data.Id_User = idUser;
                data.Status = postRequest.Status;

                _context.Posts.Add(data);
                _context.SaveChanges();

                transaction.Commit();
                transaction.Dispose();
            }

            return new ApiResponse(200);
        }

        public ApiResponse Delete(int idUser, DeleteRequest id)
        {
            if (idUser == null)
            {
                return new ApiResponse("NO_USER");
            }

            var dataDel = _context.Posts.Where(p => p.Id_User == idUser && p.Id == id.id).FirstOrDefault();

            if (dataDel == null)
            {
                return new ApiResponse("ERROR_ID_NOT_EXISTS");
            }

            _context.Posts.Remove(dataDel);

            return new ApiResponse(200);
        }

        public ApiResponse GetDetail(int idUser, PostRequest postRequest)
        {
            if (idUser == null)
            {
                return new ApiResponse("NO_USER");
            }

            if (postRequest.Id == null)
            {
                return new ApiResponse("ERROR_IDPOST_NOT_EXISTS");
            }

            var dataResult = (from p in _context.Posts
                             select new
                             {
                                 id = p.Id,
                                 content = p.Content,
                                 count_like = p.Count_Like,
                                 count_share = p.Count_Share,
                                 count_comment = p.Count_Comment,
                                 media = p.Id_Media,
                                 status = p.Status
                             }).FirstOrDefault();
            
            return new ApiResponse(dataResult);
        }

        public ApiResponse GetList(int idUser, PostRequest postRequest)
        {
            if (postRequest.page_no < 0)
            {
                postRequest.page_no = 1;
            }

            if (postRequest.page_size < 1)
            {
                postRequest.page_size = Consts.PAGE_SIZE;
            }

            int skipPage = (postRequest.page_no - 1) * postRequest.page_size;

            if (idUser == null)
            {
                return new ApiResponse("ERROR_IDUSER_NOT_EXISTS");
            }

            if (postRequest.Id == null)
            {
                return new ApiResponse("ERROR_IDPOST_NOT_EXISTS");
            }

            var posts = (from p in _context.Posts
                         select new
                         {
                             id = p.Id,
                             content = p.Content,
                             count_like = p.Count_Like,
                             count_share = p.Count_Share,
                             count_comment = p.Count_Comment,
                             media = p.Id_Media,
                             status = p.Status
                         });

            // count element 
            int countElement = posts.Count();

            // count page 
            int numPage = countElement > 0
                ? (int)Math.Ceiling(countElement / (double)postRequest.page_size)
                : 0;

            // get data page 
            var listdataPage = posts.Take(postRequest.page_no * postRequest.page_size).Skip(skipPage).ToList();
            var dataResult = new DataListRespone
            {
                Total_elements = countElement,
                Total_page = numPage,
                Page_no = postRequest.page_no,
                Page_Size = postRequest.page_size,
                Data = listdataPage,

            };

            return new ApiResponse(dataResult);
        }

        public ApiResponse Update(int idUser, PostRequest postRequest)
        {
            throw new NotImplementedException();
        }
    }
}
