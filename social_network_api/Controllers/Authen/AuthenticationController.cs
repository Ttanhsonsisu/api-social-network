using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using social_network_api.Data;
using social_network_api.DataObjects.Common;
using social_network_api.DataObjects.Request.Authen;
using social_network_api.DataObjects.Response;
using social_network_api.Extensions;
using social_network_api.Helpers;
using social_network_api.Interfaces;
using social_network_api.Interfaces.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace social_network_api.Controllers.Authen
{
    [Route("api/authen")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly ILoggingHelpers _logging;
        private readonly ICommonFunctions _commonFuntions;
        private readonly IJwt _jwtAuthen;
        private readonly IUser _ortherList;

        public AuthenticationController(IJwt jwtAuthen , ApplicationDBContext context, ILoggingHelpers logging, ICommonFunctions commonFuntions, IUser ortherList)
        {
            _context = context;
            _logging = logging;
            _commonFuntions = commonFuntions;
            _jwtAuthen = jwtAuthen;
            _ortherList = ortherList;
        }

        [Route("test")]
        [HttpGet]
        public JsonResult Test ()
        {
            var resultTest = new
            {
                content = "nnull"
            };

            return new JsonResult(new ApiResponse(resultTest)) { StatusCode = 200 };
        }

        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public async Task<JsonResult> Login(LoginRequest loginRequest)
        {
            var checkUser =  _context.Users.Where(u => u.Username == loginRequest.UserName && u.Status == 1).FirstOrDefault();

            if (checkUser == null)
            {
                return new JsonResult(new ApiResponse("ERROR_USERNAME_NOT_EXISTS")) { StatusCode = 200 };
            }
             
            if (checkUser.Password != _commonFuntions.ComputeSha256Hash(loginRequest.Password)) {
                return new JsonResult(new ApiResponse("ERROR_PASSWORD_INCORRECT")) { StatusCode = 200 };
            }

            var userType = checkUser.Is_Sysadmin ? Consts.USER_TYPE_ADMIN : Consts.USER_TYPE_MEMBER;

            var token = _jwtAuthen.Authentitcation(checkUser.Username, _commonFuntions.ComputeSha256Hash(checkUser.Password), userType);
            
            if (token == null)
            {
                return new JsonResult(new ApiResponse("ERROR_SERVER")) { StatusCode = 200 };
            }

            var resultRespone = new
            {
                user_name = checkUser.Username,
                token = token,
                email = checkUser.Email,
                name = checkUser.Full_Name,
                address = checkUser.Address
            };

            // write log 

            var remoteIP = Request.HttpContext.Connection.RemoteIpAddress;
            await _logging.InsertLogging(new LoggingRequest
            {
                User_type = userType,
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

            return new JsonResult(resultRespone) { StatusCode = 200 };
        }

        // API Đăng xuất
        [Route("logout")]
        [Authorize(Policy = "AppUser")]
        [HttpPost]
        public JsonResult Logout()
        {
            var username = User.Claims.Where(p => p.Type.Equals(ClaimTypes.Name)).FirstOrDefault();

            var remoteIP = Request.HttpContext.Connection.RemoteIpAddress;
            _logging.InsertLogging(new LoggingRequest
            {
                User_type = Consts.USER_TYPE_MEMBER,
                Is_Call_Api = true,
                Api_Name = "/api/app/auth/logout",
                Actions = "Đăng nhập với Tài khoản mới",
                Content = "",
                Functions = "Hệ thống",
                Is_Login = true,
                Result_Logging = "Thành công",
                User_Created = "",
                IP = remoteIP.ToString()
            });

            return new JsonResult(new ApiResponse(200)) { StatusCode = 200 };
        }

        public JsonResult Register(RegisterRequest req)
        {
            var username = User.Claims.Where(p => p.Type.Equals(ClaimTypes.Name)).FirstOrDefault();

            var data = _ortherList.Register(req);

            var remoteIP = Request.HttpContext.Connection.RemoteIpAddress;
            _logging.InsertLogging(new LoggingRequest
            {
                User_type = Consts.USER_TYPE_MEMBER,
                Is_Call_Api = true,
                Api_Name = "/api/app/auth/register",
                Actions = "Đăng ký tài khoản",
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
