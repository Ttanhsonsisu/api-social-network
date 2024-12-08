using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging.Abstractions;
using social_network_api.Data;
using social_network_api.DataObjects.CommentRequest;
using social_network_api.DataObjects.Request.Common;
using social_network_api.DataObjects.Response;
using social_network_api.Extensions;
using social_network_api.Interfaces.Comment;
using social_network_api.Models.Command;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mime;
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

            if (commentRequest == null)
            {
                return new ApiResponse("ERROR_CONTENT_COMMENT_MISSING");
            }

            if (commentRequest.Id_Post == null)
            {
                return new ApiResponse("ERROR_POST_NOT_EXISTS");
            }

            var postExists = _context.Posts.Where(p => p.Id == commentRequest.Id_Post).FirstOrDefault();
            if (postExists == null) 
            {
                return new ApiResponse("ERROR_POST_NOT_EXISTS");
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var data = new Comment();
                    data.Content = commentRequest.Content;
                    data.Count_Comment = 0;
                    data.Count_Like = 0;
                    data.Id_Parent = commentRequest.Id_Parent;
                    data.Id_Post = commentRequest.Id_Post;
                    data.Id_User = idUser;
                    data.Status = commentRequest.Status;
                    data.Date_Created = DateTime.Now;
                    data.Date_Updated = DateTime.Now;
                    data.User_Created = idUser.ToString();
                    data.User_Updated = idUser.ToString();

                    _context.Comments.Add(data);
                    _context.SaveChanges();

                    var dataPost = _context.Posts.Where(p => p.Id == commentRequest.Id_Post).FirstOrDefault();
                    dataPost.Count_Comment += 1;
                    _context.SaveChanges();

                    if (commentRequest.Id_Parent != null)
                    {
                        var commentUpdateCount = _context.Comments.Where(c => c.Id == commentRequest.Id_Parent).FirstOrDefault();
                        if (commentUpdateCount != null)
                        {
                            commentUpdateCount.Count_Comment += 1;
                            _context.SaveChanges();
                        }
                    }

                }
                catch (Exception ex) 
                {

                    transaction.Rollback();
                    transaction.Dispose();

                    return new ApiResponse(ex.Message.ToString());
                }
                
                transaction.Commit();
                transaction.Dispose();
            }

            return new ApiResponse(200);
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

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var data = _context.Comments.Where(c => c.Id == id.id).FirstOrDefault();
                    if (data == null)
                    {
                        return new ApiResponse("ERROR_COMMENT_NOT_EXISTS");
                    }

                    var post = _context.Posts.Where(c => c.Id == data.Id_Post).FirstOrDefault();
                    if (post == null)
                    {
                        return new ApiResponse("ERROR_POST_NOT_EXISTS");
                    }

                    _context.Comments.Remove(data);
                    _context.SaveChanges();

                    post.Count_Comment -= 1;
                    _context.SaveChanges();
                    
                    transaction.Commit();
                    transaction.Dispose();
                }
                catch (Exception ex) 
                {
                    transaction.Rollback();
                    transaction.Dispose();
                    return new ApiResponse(ex.Message.ToString());
                }
            }

            return new ApiResponse(200);
        }

        public ApiResponse GetChil(CommentRequest comemntRequest)
        {
            if (comemntRequest.Id_Parent == null)
            {
                return new ApiResponse("ERROR_ID_COMMENT_PARENT_MISSING");
            }

            if (comemntRequest.Id_Post == null)
            {
                return new ApiResponse("ERROR_ID_MISSING");
            }

            //check exists
            var postData = _context.Posts.Where(p => p.Id == comemntRequest.Id_Post).FirstOrDefault();
            if (postData == null)
            {
                return new ApiResponse("ERROR_POST_NOT_EXISTS");
            }

            // Default page_no, page_size
            if (comemntRequest.page_size < 1)
            {
                comemntRequest.page_size = Consts.PAGE_SIZE;
            }

            if (comemntRequest.page_no < 1)
            {
                comemntRequest.page_no = 1;
            }
            // Số lượng Skip
            int skipElements = (comemntRequest.page_no - 1) * comemntRequest.page_size;

            var listComment = (from c in _context.Comments
                               where c.Id_Post == comemntRequest.Id_Post && c.Id_Parent == comemntRequest.Id_Parent
                               select new
                               {
                                   id = c.Id,
                                   id_post = c.Id_Post,
                                   id_user = c.Id_User,
                                   content = c.Content,
                                   user_created = c.User_Created,
                                   user_updated = c.User_Updated,
                                   date_created = c.Date_Created,
                                   date_updated = c.Date_Updated,
                               }
                               ).ToList();

            int countElements = listComment.Count();

            int totalPage = countElements > 0
                    ? (int)Math.Ceiling(countElements / (double)comemntRequest.page_size)
                    : 0;

            var dataList = listComment.Take(comemntRequest.page_size * comemntRequest.page_no).Skip(skipElements).ToList();
            var dataResult = new DataListRespone { Page_no = comemntRequest.page_no, Page_Size = comemntRequest.page_size, Total_elements = countElements, Total_page = totalPage, Data = dataList };
            return new ApiResponse(dataResult);
        }

        public ApiResponse GetDetail(int idUser, int? id)
        {
            if (idUser == null)
            {
                return new ApiResponse("ERROR_ID_USER_MISSING");
            }

            if (id == null)
            {
                return new ApiResponse("ERROR_ID_COMMENT_MISSING");
            }

            var data = _context.Comments.Where(c => c.Id == id).FirstOrDefault();
            if (data == null)
            {
                return new ApiResponse("ERROR_COMMENT_NOT_EXISTS");
            }

            return new ApiResponse(data);
        }

        public ApiResponse GetListParent(CommentRequest comemntRequest)
        {
            if (comemntRequest.Id == null)
            {
                return new ApiResponse("ERROR_ID_COMMENT_MISSING");
            }

            if (comemntRequest.Id_Post == null)
            {
                return new ApiResponse("ERROR_ID_MISSING");
            }

            //check exists
            var postData = _context.Posts.Where(p => p.Id == comemntRequest.Id_Post).FirstOrDefault();
            if (postData == null)
            {
                return new ApiResponse("ERROR_POST_NOT_EXISTS");
            }

            // Default page_no, page_size
            if (comemntRequest.page_size < 1)
            {
                comemntRequest.page_size = Consts.PAGE_SIZE;
            }

            if (comemntRequest.page_no < 1)
            {
                comemntRequest.page_no = 1;
            }
            // Số lượng Skip
            int skipElements = (comemntRequest.page_no - 1) * comemntRequest.page_size;

            var listComment = (from c in _context.Comments
                               where c.Id_Post == comemntRequest.Id_Post && c.Id_Parent == null
                               select new
                               {
                                   id = c.Id,
                                   id_post = c.Id_Post,
                                   id_user = c.Id_User,
                                   content = c.Content,
                                   user_created = c.User_Created,
                                   user_updated = c.User_Updated,
                                   date_created = c.Date_Created,
                                   date_updated = c.Date_Updated,
                               }
                               ).ToList();

            int countElements = listComment.Count();

            int totalPage = countElements > 0
                    ? (int)Math.Ceiling(countElements / (double)comemntRequest.page_size)
                    : 0;

            var dataList = listComment.Take(comemntRequest.page_size * comemntRequest.page_no).Skip(skipElements).ToList();
            var dataResult = new DataListRespone { Page_no = comemntRequest.page_no, Page_Size = comemntRequest.page_size, Total_elements = countElements, Total_page = totalPage, Data = dataList };
            return new ApiResponse(dataResult);
        }

        public ApiResponse GetListTree(CommentRequest comemntRequest)
        {
            if (comemntRequest.Id_Post == null) 
            {
                return new ApiResponse("ERROR_ID_MISSING");
            }

            //check exists
            var postData = _context.Posts.Where(p => p.Id == comemntRequest.Id_Post).FirstOrDefault();
            if (postData == null)
            {
                return new ApiResponse("ERROR_POST_NOT_EXISTS");
            }

            // Default page_no, page_size
            if (comemntRequest.page_size < 1)
            {
                comemntRequest.page_size = Consts.PAGE_SIZE;
            }

            if (comemntRequest.page_no < 1)
            {
                comemntRequest.page_no = 1;
            }
            // Số lượng Skip
            int skipElements = (comemntRequest.page_no - 1) * comemntRequest.page_size;

            var listComment = (from c in _context.Comments 
                               where c.Id_Post == comemntRequest.Id_Post
                               select new
                               {
                                   Id = c.Id,
                                   Id_Post = c.Id_Post,
                                   Id_User = c.Id_User,
                                   Content = c.Content,
                                   Id_Parent = c.Id_Parent,
                                   Comment_Chil = (from ch in _context.Comments
                                                   where ch.Id_Parent == c.Id
                                                   select new
                                                   {
                                                       id_comment = ch.Id,
                                                       id_post = ch.Id_Post,
                                                       id_user = ch.Id_User,
                                                       content = ch.Content,
                                                       
                                                       user_created = ch.User_Created,
                                                       user_updated = ch.User_Updated,
                                                       date_created = ch.Date_Created,
                                                       date_updated = ch.Date_Updated,
                                                   }
                                                   ).ToList(),
                                   user_created = c.User_Created,
                                   user_updated = c.User_Updated,
                                   date_created = c.Date_Created,
                                   date_updated = c.Date_Updated,
                               }
                               ).ToList();

            int countElements = listComment.Count();

            int totalPage = countElements > 0
                    ? (int)Math.Ceiling(countElements / (double)comemntRequest.page_size)
                    : 0;

            var dataList = listComment.Take(comemntRequest.page_size * comemntRequest.page_no).Skip(skipElements).ToList();
            var dataResult = new DataListRespone { Page_no = comemntRequest.page_no, Page_Size = comemntRequest.page_size, Total_elements = countElements, Total_page = totalPage, Data = dataList };
            return new ApiResponse(dataResult);

        }

        public ApiResponse Update(int idUser, CommentRequest commentRequest)
        {
            if (idUser == null)
            {
                return new ApiResponse("ERROR_MISSING_USER_ID");
            }

            if (commentRequest.Id == null)
            {
                return new ApiResponse("ERROR_MISSING_ID_COMMENT");
            }

            if (commentRequest.Content == null)
            {
                return new ApiResponse("ERROR_MISSING_CONTENT_COMMENT");
            }

            try
            {
                var data = _context.Comments.Where(c => c.Id == commentRequest.Id).FirstOrDefault();
                if (data == null)
                {
                    return new ApiResponse("ERROR_NOT_EXISTS_COMMENT");
                }

                data.Status = commentRequest.Status;
                data.Content = commentRequest.Content;
                data.Date_Updated = DateTime.Now;
                data.User_Updated = idUser.ToString();

                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                return new ApiResponse(ex.Message.ToString());
            }
            return new ApiResponse(200);
        }
    }
}
