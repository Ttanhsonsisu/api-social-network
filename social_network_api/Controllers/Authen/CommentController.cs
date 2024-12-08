using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using social_network_api.Data;
using social_network_api.DataObjects.CommentRequest;
using social_network_api.DataObjects.Common;
using social_network_api.DataObjects.Request.Common;
using social_network_api.Extensions;
using social_network_api.Helpers;
using social_network_api.Interfaces.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace social_network_api.Controllers.Authen
{
    [Route("/api/auth/comment")]
    [Authorize(Policy = "AppUser")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ILoggingHelpers _logging;
        private readonly ApplicationDBContext _context;
        private readonly IComment _listOtherData;

        public CommentController(ILoggingHelpers logging, ApplicationDBContext context, IComment listOtherData)
        {
            _logging = logging;
            _context = context;
            _listOtherData = listOtherData;
        }

        [Route("create")]
        [HttpPost]
        public JsonResult Create(CommentRequest commentRequest)
        {
            var username = User.Claims.Where(p => p.Type.Equals(ClaimTypes.Name)).FirstOrDefault();
            var user = _context.Users.Where(u => u.Username == username.Value).FirstOrDefault();

            var data = _listOtherData.Create(user.Id, commentRequest);

            var remoteIP = Request.HttpContext.Connection.RemoteIpAddress;

            _logging.InsertLogging(new LoggingRequest
            {
                User_type = Consts.USER_TYPE_MEMBER,
                Is_Call_Api = true,
                Api_Name = "/api/app/auth/login",
                Actions = "Tạo mới comment" + username.ToString(),
                Content = "",
                Functions = "Hệ thống",
                Is_Login = true,
                Result_Logging = "Thành công",
                User_Created = "",
                IP = remoteIP.ToString()
            });

            return new JsonResult(data) { StatusCode = 200 };
        }

        [Route("update")]
        [HttpPost]
        public async Task<JsonResult> Update(CommentRequest commentRequest)
        {
            var username = User.Claims.Where(p => p.Type.Equals(ClaimTypes.Name)).FirstOrDefault();
            var user = _context.Users.Where(u => u.Username == username.Value).FirstOrDefault();

            var data = _listOtherData.Update(user.Id, commentRequest);
            // write log 

            var remoteIP = Request.HttpContext.Connection.RemoteIpAddress;

            await _logging.InsertLogging(new LoggingRequest
            {
                User_type = Consts.USER_TYPE_MEMBER,
                Is_Call_Api = true,
                Api_Name = "/api/app/auth/login",
                Actions = "Cập nhật comment" + username.Value.ToString() + commentRequest.Id.ToString(),
                Content = "",
                Functions = "Hệ thống",
                Is_Login = true,
                Result_Logging = "Thành công",
                User_Created = "",
                IP = remoteIP.ToString()
            });

            return new JsonResult(data) { StatusCode = 200 };
        }

        [HttpGet("detail/{id}")]
        public  JsonResult GetDetail(int id)
        {
            var username = User.Claims.Where(p => p.Type.Equals(ClaimTypes.Name)).FirstOrDefault();
            var user = _context.Users.Where(u => u.Username == username.Value).FirstOrDefault();

            var data = _listOtherData.GetDetail(user.Id, id);
            // write log 

            var remoteIP = Request.HttpContext.Connection.RemoteIpAddress;

            _logging.InsertLogging(new LoggingRequest
            {
                User_type = Consts.USER_TYPE_MEMBER,
                Is_Call_Api = true,
                Api_Name = "/api/app/auth/login",
                Actions = "xem chi tiết comment" + id.ToString(),
                Content = "",
                Functions = "Hệ thống",
                Is_Login = true,
                Result_Logging = "Thành công",
                User_Created = "",
                IP = remoteIP.ToString()
            });

            return new JsonResult(data) { StatusCode = 200 };
        }

        [Route("listCommentTree")]
        [HttpPost]
        public JsonResult GetCommentListTree(CommentRequest commentRequest)
        {
            var username = User.Claims.Where(p => p.Type.Equals(ClaimTypes.Name)).FirstOrDefault();
            var user = _context.Users.Where(u => u.Username == username.Value).FirstOrDefault();

            var data = _listOtherData.GetListTree(commentRequest);
            
            // write log
            var remoteIP = Request.HttpContext.Connection.RemoteIpAddress;

            _logging.InsertLogging(new LoggingRequest
            {
                User_type = Consts.USER_TYPE_MEMBER,
                Is_Call_Api = true,
                Api_Name = "/api/app/auth/getlist",
                Actions = "lấy ra list các comment" + username.Value.ToString(),
                Content = "",
                Functions = "Hệ thống",
                Is_Login = true,
                Result_Logging = "Thành công",
                User_Created = "",
                IP = remoteIP.ToString()
            });

            return new JsonResult(data) { StatusCode = 200 };
        }

        [Route("listCommentChil")]
        [HttpPost]
        public JsonResult GetCommentListChil(CommentRequest commentRequest)
        {
            var username = User.Claims.Where(p => p.Type.Equals(ClaimTypes.Name)).FirstOrDefault();
            var user = _context.Users.Where(u => u.Username == username.Value).FirstOrDefault();

            var data = _listOtherData.GetChil(commentRequest);

            // write log
            var remoteIP = Request.HttpContext.Connection.RemoteIpAddress;

            _logging.InsertLogging(new LoggingRequest
            {
                User_type = Consts.USER_TYPE_MEMBER,
                Is_Call_Api = true,
                Api_Name = "/api/app/auth/getlist",
                Actions = "lấy ra list các comment" + username.Value.ToString(),
                Content = "",
                Functions = "Hệ thống",
                Is_Login = true,
                Result_Logging = "Thành công",
                User_Created = "",
                IP = remoteIP.ToString()
            });

            return new JsonResult(data) { StatusCode = 200 };
        }

        [Route("listCommentParent")]
        [HttpPost]
        public JsonResult GetCommentListParent(CommentRequest commentRequest)
        {
            var username = User.Claims.Where(p => p.Type.Equals(ClaimTypes.Name)).FirstOrDefault();
            var user = _context.Users.Where(u => u.Username == username.Value).FirstOrDefault();

            var data = _listOtherData.GetListParent(commentRequest);

            // write log
            var remoteIP = Request.HttpContext.Connection.RemoteIpAddress;

            _logging.InsertLogging(new LoggingRequest
            {
                User_type = Consts.USER_TYPE_MEMBER,
                Is_Call_Api = true,
                Api_Name = "/api/app/auth/getlist",
                Actions = "lấy ra list các comment" + username.Value.ToString(),
                Content = "",
                Functions = "Hệ thống",
                Is_Login = true,
                Result_Logging = "Thành công",
                User_Created = "",
                IP = remoteIP.ToString()
            });

            return new JsonResult(data) { StatusCode = 200 };
        }

        [Route("delete")]
        [HttpPost]
        public JsonResult Delete(DeleteRequest deleteRequest)
        {
            var username = User.Claims.Where(p => p.Type.Equals(ClaimTypes.Name)).FirstOrDefault();
            var user = _context.Users.Where(u => u.Username == username.Value).FirstOrDefault();

            var data = _listOtherData.Delete(user.Id, deleteRequest);
            // write log 

            var remoteIP = Request.HttpContext.Connection.RemoteIpAddress;

            _logging.InsertLogging(new LoggingRequest
            {
                User_type = Consts.USER_TYPE_MEMBER,
                Is_Call_Api = true,
                Api_Name = "/api/app/auth/login",
                Actions = "xóa comment" + deleteRequest.id.ToString(),
                Content = "",
                Functions = "Hệ thống",
                Is_Login = true,
                Result_Logging = "Thành công",
                User_Created = "",
                IP = remoteIP.ToString()
            });

            return new JsonResult(data) { StatusCode = 200 };
        }
    }
}
