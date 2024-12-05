using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using social_network_api.Data;
using social_network_api.DataObjects.Common;
using social_network_api.DataObjects.Request.Authen;
using social_network_api.DataObjects.Response;
using social_network_api.Extensions;
using social_network_api.Helpers;
using social_network_api.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace social_network_api.Controllers.cms.Authen
{
    [ApiController]
    [Authorize(Policy = "WebAdminUser")]
    [Route("api/auth")]
    public class AuthenticationController : ControllerBase
    {

        private readonly IJwt jwtAuth;
        private readonly ILoggingHelpers _logging;
        private readonly ApplicationDBContext _context;
        private readonly ICommonFunctions _commonFunction;

        public AuthenticationController(IJwt jwtAuth, ILoggingHelpers logging, ApplicationDBContext context, ICommonFunctions commonFunction)
        {
            this.jwtAuth = jwtAuth;
            _logging = logging;
            _context = context;
            _commonFunction = commonFunction;
        }

        // API Đăng nhập Web Admin
        [AllowAnonymous]
        [Route("adminLogin")]
        [HttpPost]
        public JsonResult AdminLogin(LoginRequest loginRequest)
        {
            // Check Username
            var checkUserName = _context.Users.Where(x => x.Username == loginRequest.UserName && x.Is_Sysadmin == true && x.Status == 1).FirstOrDefault();
            if (checkUserName == null)
            {
                return new JsonResult(new ApiResponse("ERROR_USERNAME_NOT_EXISTS")) { StatusCode = 200 };
            }

            if (checkUserName.Password != _commonFunction.ComputeSha256Hash(loginRequest.Password))
            {
                return new JsonResult(new ApiResponse("ERROR_PASSWORD_INCORRECT")) { StatusCode = 200 };
            }

            var token = jwtAuth.Authentitcation(loginRequest.UserName, _commonFunction.ComputeSha256Hash(loginRequest.Password), Consts.USER_TYPE_ADMIN);
            if (token == null)
            {
                return new JsonResult(new ApiResponse("ERROR_SERVER")) { StatusCode = 200 };
            }

            // get list user permision
            /**
            var listFunctionIds = (from p in _context.Actions
                                   join up in _context.UserPermissions on p.id equals up.action_id
                                   join f in _context.Functions on p.function_id equals f.id
                                   where up.user_id == checkUserName.id
                                   select f.id).ToList();

            var user_permissions = (from p in _context.Functions
                                    where p.status == 1 && listFunctionIds.Contains(p.id)
                                    select new
                                    {
                                        id = p.id,
                                        function_code = p.code,
                                        function_name = p.name,
                                        path = p.url,
                                        actions = (from i in _context.Actions
                                                   join up in _context.UserPermissions on i.id equals up.action_id
                                                   where i.function_id == p.id && up.user_id == checkUserName.id
                                                   select new
                                                   {
                                                       action_id = i.id,
                                                       action_code = i.code,
                                                       action_name = i.name,
                                                       path = i.url
                                                   }).ToList()
                                    }).ToList();
            **/

            // Login Response
            object loginResponse = new
            {
                user_name = checkUserName.Username,
                token = token,
                email = checkUserName.Email,
                name = checkUserName.Full_Name,
                address = checkUserName.Address
            };

            // Ghi log
            var remoteIP = Request.HttpContext.Connection.RemoteIpAddress;
            _logging.InsertLogging(new LoggingRequest
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
            return new JsonResult(new ApiResponse(loginResponse)) { StatusCode = 200 };
        }

        // API Đăng xuất Web Admin
        [Route("adminLogout")]
        [Authorize(Policy = "WebAdminUser")]
        [HttpPost]
        public JsonResult AdminLogout()
        {
            var username = User.Claims.Where(p => p.Type.Equals(ClaimTypes.Name)).FirstOrDefault();

            // Ghi log
            var remoteIP = Request.HttpContext.Connection.RemoteIpAddress;
            _logging.InsertLogging(new LoggingRequest
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

            return new JsonResult(new ApiResponse(200)) { StatusCode = 200 };
        }

        // forgot pass 

        //
    }
}
