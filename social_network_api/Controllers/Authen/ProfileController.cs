using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using social_network_api.Data;
using social_network_api.DataObjects.CommentRequest;
using social_network_api.DataObjects.Common;
using social_network_api.DataObjects.Request.Authen;
using social_network_api.Extensions;
using social_network_api.Helpers;
using social_network_api.Interfaces.Info;
using System.Linq;
using System.Security.Claims;

namespace social_network_api.Controllers.Authen
{
    [ApiController]
    [Route("/api/authen/profile")]
    [Authorize(Policy = "AppUser")]
    public class ProfileController : ControllerBase
    {
        private readonly ILoggingHelpers _logging;
        private readonly ApplicationDBContext _context;
        private readonly IProfile _listOtherData;

        public ProfileController(ILoggingHelpers logging, ApplicationDBContext context, IProfile listOtherData)
        {
            _logging = logging;
            _context = context;
            _listOtherData = listOtherData;
        }

        [HttpGet]
        public JsonResult Get()
        {
            var username = User.Claims.Where(p => p.Type.Equals(ClaimTypes.Name)).FirstOrDefault();

            //var user = _context.Users.Where(u => u.Username == username.Value).FirstOrDefault();

            var data = _listOtherData.Get(username.Value.ToString());

            var remoteIP = Request.HttpContext.Connection.RemoteIpAddress;

            _logging.InsertLogging(new LoggingRequest
            {
                User_type = Consts.USER_TYPE_MEMBER,
                Is_Call_Api = true,
                Api_Name = "/api/authen/profile/create",
                Actions = "lấy thông tin user profile" + username.ToString(),
                Content = "",
                Functions = "Hệ thống",
                Is_Login = true,
                Result_Logging = "Thành công",
                User_Created = "",
                IP = remoteIP.ToString()
            });

            return new JsonResult(data) { StatusCode = 200 };
        }

        [Route("create")]
        [HttpPost]
        public JsonResult Create(ProfileRequest req)
        {
            var username = User.Claims.Where(p => p.Type.Equals(ClaimTypes.Name)).FirstOrDefault();

            //var user = _context.Users.Where(u => u.Username == username.Value).FirstOrDefault();

            var data = _listOtherData.Create(username.Value.ToString() , req);

            var remoteIP = Request.HttpContext.Connection.RemoteIpAddress;

            _logging.InsertLogging(new LoggingRequest
            {
                User_type = Consts.USER_TYPE_MEMBER,
                Is_Call_Api = true,
                Api_Name = "/api/authen/profile/create",
                Actions = "Tạo mới profile" + username.ToString(),
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
        public JsonResult Update(ProfileRequest req)
        {
            var username = User.Claims.Where(p => p.Type.Equals(ClaimTypes.Name)).FirstOrDefault();

            //var user = _context.Users.Where(u => u.Username == username.Value).FirstOrDefault();

            var data = _listOtherData.Update(username.Value.ToString() , req);

            var remoteIP = Request.HttpContext.Connection.RemoteIpAddress;

            _logging.InsertLogging(new LoggingRequest
            {
                User_type = Consts.USER_TYPE_MEMBER,
                Is_Call_Api = true,
                Api_Name = "/api/authen/profile/update",
                Actions = "Sửa profile" + username.ToString(),
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
        public JsonResult Delete(ProfileRequest req)
        {
            var username = User.Claims.Where(p => p.Type.Equals(ClaimTypes.Name)).FirstOrDefault();

            //var user = _context.Users.Where(u => u.Username == username.Value).FirstOrDefault();

            var data = _listOtherData.Delete(username.Value.ToString());

            var remoteIP = Request.HttpContext.Connection.RemoteIpAddress;

            _logging.InsertLogging(new LoggingRequest
            {
                User_type = Consts.USER_TYPE_MEMBER,
                Is_Call_Api = true,
                Api_Name = "/api/authen/profile/delete",
                Actions = "Xóa profile" + username.ToString(),
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
