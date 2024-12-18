using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using social_network_api.DataObjects.CommentRequest;
using social_network_api.DataObjects.Common;
using social_network_api.DataObjects.Request.Authen;
using social_network_api.Extensions;
using social_network_api.Helpers;
using social_network_api.Interfaces.Info;
using social_network_api.Models.Common;
using System.Linq;
using System.Security.Claims;

namespace social_network_api.Controllers.Authen
{

    [ApiController]
    [Route("api/authen/education")]
    [Authorize(Policy = "AppUser")]
    public class EducationController : Controller
    {
        private readonly ICommonFunctions _commonFunctions;
        private readonly IEducation _listOtherData;
        private readonly ILoggingHelpers _logging;

        public EducationController(ICommonFunctions commonFunctions, IEducation listOtherData, ILoggingHelpers logger)
        {
            _commonFunctions = commonFunctions;
            _listOtherData = listOtherData;
            _logging = logger;
        }

        [Route("create")]
        [HttpPost]
        public JsonResult Create (EducationRequest req)
        {
            var username = User.Claims.Where(p => p.Type.Equals(ClaimTypes.Name)).FirstOrDefault();

            var data = _listOtherData.Create(username.Value.ToString(), req);


            var remoteIP = Request.HttpContext.Connection.RemoteIpAddress;

            _logging.InsertLogging(new LoggingRequest
            {
                User_type = Consts.USER_TYPE_MEMBER,
                Is_Call_Api = true,
                Api_Name = "/api/auth/education/create",
                Actions = "Tạo mới education" + username.ToString(),
                Content = "",
                Functions = "Hệ thống",
                Is_Login = true,
                Result_Logging = "Thành công",
                User_Created = "",
                IP = remoteIP.ToString()
            });

            return new JsonResult(data) { StatusCode = 200 };
        }


        [Route("List")]
        [HttpPost]
        public JsonResult GetList(EducationRequest req)
        {
            var username = User.Claims.Where(p => p.Type.Equals(ClaimTypes.Name)).FirstOrDefault();

            var data = _listOtherData.GetAll(username.Value.ToString(), req);


            var remoteIP = Request.HttpContext.Connection.RemoteIpAddress;

            _logging.InsertLogging(new LoggingRequest
            {
                User_type = Consts.USER_TYPE_MEMBER,
                Is_Call_Api = true,
                Api_Name = "/api/auth/education/list",
                Actions = "Lấy toàn bộ education của người dùng " + username.ToString(),
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
        public JsonResult GetDetail(int id)
        {
            var username = User.Claims.Where(p => p.Type.Equals(ClaimTypes.Name)).FirstOrDefault();

            var data = _listOtherData.GetDetail(username.Value.ToString(), id);


            var remoteIP = Request.HttpContext.Connection.RemoteIpAddress;

            _logging.InsertLogging(new LoggingRequest
            {
                User_type = Consts.USER_TYPE_MEMBER,
                Is_Call_Api = true,
                Api_Name = "/api/auth/education/list",
                Actions = "Lấy toàn bộ education của người dùng " + username.ToString(),
                Content = "",
                Functions = "Hệ thống",
                Is_Login = true,
                Result_Logging = "Thành công",
                User_Created = "",
                IP = remoteIP.ToString()
            });

            return new JsonResult(data) { StatusCode = 200 };
        }


        [HttpGet("update")]
        public JsonResult Update(EducationRequest req)
        {
            var username = User.Claims.Where(p => p.Type.Equals(ClaimTypes.Name)).FirstOrDefault();

            var data = _listOtherData.Update(username.Value.ToString(), req);

            var remoteIP = Request.HttpContext.Connection.RemoteIpAddress;

            _logging.InsertLogging(new LoggingRequest
            {
                User_type = Consts.USER_TYPE_MEMBER,
                Is_Call_Api = true,
                Api_Name = "/api/auth/education/update",
                Actions = "Cập nhật thông tin education của người dùng " + username.ToString(),
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
