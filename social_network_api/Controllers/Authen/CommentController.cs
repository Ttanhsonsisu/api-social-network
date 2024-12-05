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
    [Route("/api/auth/post")]
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
        public async Task<JsonResult> Create(CommentRequest commentRequest)
        {
            var username = User.Claims.Where(p => p.Type.Equals(ClaimTypes.Name)).FirstOrDefault();
            var user = _context.Users.Where(u => u.Username == username.Value).FirstOrDefault();

            var data = _listOtherData.Create(user.Id, commentRequest);

            var remoteIP = Request.HttpContext.Connection.RemoteIpAddress;

            await _logging.InsertLogging(new LoggingRequest
            {
                User_type = Consts.USER_TYPE_MEMBER,
                Is_Call_Api = true,
                Api_Name = "/api/app/auth/login",
                Actions = "Đăng nhập với Tài khoản mới",
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
                Actions = "Đăng nhập với Tài khoản mới",
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
        public async Task<JsonResult> GetDetail(CommentRequest commentRequest)
        {
            var username = User.Claims.Where(p => p.Type.Equals(ClaimTypes.Name)).FirstOrDefault();
            var user = _context.Users.Where(u => u.Username == username.Value).FirstOrDefault();

            var data = _listOtherData.GetDetail(user.Id, commentRequest.Id);
            // write log 

            var remoteIP = Request.HttpContext.Connection.RemoteIpAddress;

            await _logging.InsertLogging(new LoggingRequest
            {
                User_type = Consts.USER_TYPE_MEMBER,
                Is_Call_Api = true,
                Api_Name = "/api/app/auth/login",
                Actions = "Đăng nhập với Tài khoản mới",
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
        public async Task<JsonResult> GetList(CommentRequest commentRequest)
        {
            var username = User.Claims.Where(p => p.Type.Equals(ClaimTypes.Name)).FirstOrDefault();
            var user = _context.Users.Where(u => u.Username == username.Value).FirstOrDefault();

            var data = _listOtherData.GetList(user.Id);
            // write log 

            var remoteIP = Request.HttpContext.Connection.RemoteIpAddress;

            await _logging.InsertLogging(new LoggingRequest
            {
                User_type = Consts.USER_TYPE_MEMBER,
                Is_Call_Api = true,
                Api_Name = "/api/app/auth/getlist",
                Actions = "lấy ra list các comment",
                Content = "",
                Functions = "Hệ thống",
                Is_Login = true,
                Result_Logging = "Thành công",
                User_Created = "",
                IP = remoteIP.ToString()
            });

            return new JsonResult("") { StatusCode = 200 };
        }

        [Route("delete")]
        [HttpPost]
        public async Task<JsonResult> Delete(DeleteRequest deleteRequest)
        {
            var username = User.Claims.Where(p => p.Type.Equals(ClaimTypes.Name)).FirstOrDefault();
            var user = _context.Users.Where(u => u.Username == username.Value).FirstOrDefault();

            var data = _listOtherData.Delete(user.Id, deleteRequest);
            // write log 

            var remoteIP = Request.HttpContext.Connection.RemoteIpAddress;

            await _logging.InsertLogging(new LoggingRequest
            {
                User_type = Consts.USER_TYPE_MEMBER,
                Is_Call_Api = true,
                Api_Name = "/api/app/auth/login",
                Actions = "xóa comment",
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
