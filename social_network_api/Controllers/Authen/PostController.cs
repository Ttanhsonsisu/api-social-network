using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using social_network_api.Data;
using social_network_api.DataObjects.ActionObject.Post;
using social_network_api.DataObjects.Common;
using social_network_api.DataObjects.Request.Common;

using social_network_api.Extensions;
using social_network_api.Helpers;
using social_network_api.Interfaces.Post;

namespace social_network_api.Controllers.Authen
{
    [Route("/api/auth/post")]
    [Authorize(Policy = "AppUser")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPost _ortherList;
        private readonly ILoggingHelpers _logging;
        private readonly ApplicationDBContext _context;
        public PostController(IPost ortherList, ILoggingHelpers logging , ApplicationDBContext context)
        {
            _ortherList = ortherList;
            _logging = logging;
            _context = context;
        }

        [Route("create")]
        [HttpPost]
        public async Task<JsonResult> Create (PostRequest postRequest )
        {
            var username = User.Claims.Where(p => p.Type.Equals(ClaimTypes.Name)).FirstOrDefault();
            var user = _context.Users.Where(u => u.Username == username.Value).FirstOrDefault();

            var data = _ortherList.Create(user.Id , postRequest);
            // write log 

            var remoteIP = Request.HttpContext.Connection.RemoteIpAddress;

            await _logging.InsertLogging(new LoggingRequest
            {
                User_type = Consts.USER_TYPE_MEMBER,
                Is_Call_Api = true,
                Api_Name = "/api/app/auth/login",
                Actions = "Tạo mới post bởi userid" + user.Id,
                Content = "",
                Functions = "Hệ thống",
                Is_Login = true,
                Result_Logging = "Thành công",
                User_Created = "",
                IP = remoteIP.ToString()
            });

            return new JsonResult(data) {StatusCode = 200};
        }

        [Route("update")]
        [HttpPost]
        public async Task<JsonResult> Update (PostRequest postRequest)
        {
            var username = User.Claims.Where(p => p.Type.Equals(ClaimTypes.Name)).FirstOrDefault();
            var user = _context.Users.Where(u => u.Username == username.Value).FirstOrDefault();

            var data = _ortherList.Update(user.Id , postRequest);
            // write log 

            var remoteIP = Request.HttpContext.Connection.RemoteIpAddress;

            await _logging.InsertLogging(new LoggingRequest
            {
                User_type = Consts.USER_TYPE_MEMBER,
                Is_Call_Api = true,
                Api_Name = "/api/app/auth/login",
                Actions = "Cập nhật post với userid" + user.Id,
                Content = "",
                Functions = "Hệ thống",
                Is_Login = true,
                Result_Logging = "Thành công",
                User_Created = "",
                IP = remoteIP.ToString()
            });

            return new JsonResult(data) { StatusCode = 200 };
        }

        [HttpGet("{id}")]
        public async Task<JsonResult> GetDetail(PostRequest postRequest)
        {
            var username = User.Claims.Where(p => p.Type.Equals(ClaimTypes.Name)).FirstOrDefault();
            var user = _context.Users.Where(u => u.Username == username.Value).FirstOrDefault();

            var data = _ortherList.GetDetail(user.Id, postRequest);
            // write log 

            var remoteIP = Request.HttpContext.Connection.RemoteIpAddress;

            await _logging.InsertLogging(new LoggingRequest
            {
                User_type = Consts.USER_TYPE_MEMBER,
                Is_Call_Api = true,
                Api_Name = "/api/app/auth/login",
                Actions = "lấy thông tin chi tiết post bởi userid" + user.Id + " postid" + postRequest.Id,
                Content = "",
                Functions = "Hệ thống",
                Is_Login = true,
                Result_Logging = "Thành công",
                User_Created = "",
                IP = remoteIP.ToString()
            });

            return new JsonResult(data) { StatusCode = 200 };
        }

        [Route("list")]
        [HttpPost]
        public async Task<JsonResult> GetList(PostRequest postRequest)
        {
            var username = User.Claims.Where(p => p.Type.Equals(ClaimTypes.Name)).FirstOrDefault();
            var user = _context.Users.Where(u => u.Username == username.Value).FirstOrDefault();

            var data = _ortherList.GetList(user.Id, postRequest);
            // write log 

            var remoteIP = Request.HttpContext.Connection.RemoteIpAddress;

            await _logging.InsertLogging(new LoggingRequest
            {
                User_type = Consts.USER_TYPE_MEMBER,
                Is_Call_Api = true,
                Api_Name = "/api/app/auth/login",
                Actions = "list danh sách post",
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
        public async Task<JsonResult> Delete(DeleteRequest deleteRequest)
        {
            var username = User.Claims.Where(p => p.Type.Equals(ClaimTypes.Name)).FirstOrDefault();
            var user = _context.Users.Where(u => u.Username == username.Value).FirstOrDefault();

            var data = _ortherList.Delete(user.Id, deleteRequest);
            // write log 

            var remoteIP = Request.HttpContext.Connection.RemoteIpAddress;

            await _logging.InsertLogging(new LoggingRequest
            {
                User_type = Consts.USER_TYPE_MEMBER,
                Is_Call_Api = true,
                Api_Name = "/api/app/auth/post/delete" + deleteRequest.id,
                Actions = "xóa post",
                Content = "",
                Functions = "app",
                Is_Login = true,
                Result_Logging = "Thành công",
                User_Created = "",
                IP = remoteIP.ToString()
            });

            return new JsonResult(data) { StatusCode = 200 };
        }

    }
}
