using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using social_network_api.Data;
using social_network_api.DataObjects.Common;
using social_network_api.DataObjects.Request.Authen;
using social_network_api.DataObjects.Request.Common;
using social_network_api.DataObjects.Response;
using social_network_api.Extensions;
using social_network_api.Helpers;
using social_network_api.Interfaces.AddFriend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace social_network_api.Controllers.Authen
{
    [ApiController]
    [Route("api/app/FriendShip")]
    [Authorize(Policy = "AppUser")]
    public class FriendController : ControllerBase
    {
        private readonly IFriend _listOtherData;
        private readonly ApplicationDBContext _context;
        private readonly ILoggingHelpers _logging;

        public FriendController(IFriend friend, ILoggingHelpers logging, ApplicationDBContext context)
        {
            _listOtherData = friend;
            _logging = logging;
            _context = context;
        }

        [Route("create")]
        [HttpPost]
        public  JsonResult Create(FriendRequest friendRequest)
        {
            var username = User.Claims.Where(p => p.Type.Equals(ClaimTypes.Name)).FirstOrDefault();
            var user = _context.Users.Where(u => u.Username == username.Value).FirstOrDefault();

            var data = _listOtherData.AddFriend(user.Id, friendRequest, username.ToString());

            var remoteIP = Request.HttpContext.Connection.RemoteIpAddress;

            _logging.InsertLogging(new LoggingRequest
            {
                User_type = Consts.USER_TYPE_MEMBER,
                Is_Call_Api = true,
                Api_Name = "/api/app/friendShip/create",
                Actions = "Thêm mới bạn bè",
                Content = "",
                Functions = "Hệ thống",
                Is_Login = true,
                Result_Logging = "Thành công",
                User_Created = "",
                IP = remoteIP.ToString()
            });

            return new JsonResult(data) { StatusCode = 200 };
        }

        [Route("remove")]
        [HttpPost]
        public JsonResult Remove(DeleteRequest req)
        {
            var username = User.Claims.Where(p => p.Type.Equals(ClaimTypes.Name)).FirstOrDefault();
            var user = _context.Users.Where(u => u.Username == username.Value).FirstOrDefault();

            var data = _listOtherData.DeleteFriend(user.Id, req);

            var remoteIP = Request.HttpContext.Connection.RemoteIpAddress;

            _logging.InsertLogging(new LoggingRequest
            {
                User_type = Consts.USER_TYPE_MEMBER,
                Is_Call_Api = true,
                Api_Name = "/api/app/friendShip/remove",
                Actions = "xóa bạn bè",
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
        public JsonResult List(FriendRequest friendRequest)
        {
            var username = User.Claims.Where(p => p.Type.Equals(ClaimTypes.Name)).FirstOrDefault();
            var user = _context.Users.Where(u => u.Username == username.Value).FirstOrDefault();

            var data = _listOtherData.GetList(user.Id, friendRequest);

            var remoteIP = Request.HttpContext.Connection.RemoteIpAddress;

             _logging.InsertLogging(new LoggingRequest
            {
                User_type = Consts.USER_TYPE_MEMBER,
                Is_Call_Api = true,
                Api_Name = "/api/app/friendShip/list",
                Actions = "list danh sách bạn bè",
                Content = "",
                Functions = "Hệ thống",
                Is_Login = true,
                Result_Logging = "Thành công",
                User_Created = "",
                IP = remoteIP.ToString()
            });

            return new JsonResult(data) { StatusCode = 200 };
        }

        [Route("listRequestAddFriend")]
        [HttpPost]
        public  JsonResult ListRequestAddFriend(FriendRequest friendRequest)
        {
            var username = User.Claims.Where(p => p.Type.Equals(ClaimTypes.Name)).FirstOrDefault();
  

            var data = _listOtherData.GetListRequestFriend(username.Value.ToString(), friendRequest);

            var remoteIP = Request.HttpContext.Connection.RemoteIpAddress;

             _logging.InsertLogging(new LoggingRequest
            {
                User_type = Consts.USER_TYPE_MEMBER,
                Is_Call_Api = true,
                Api_Name = "/api/app/friendShip/listRequestAddFriend",
                Actions = "list danh sách yêu cầu kết bạn",
                Content = "",
                Functions = "Hệ thống",
                Is_Login = true,
                Result_Logging = "Thành công",
                User_Created = "",
                IP = remoteIP.ToString()
            });

            return new JsonResult(data) { StatusCode = 200 };
        }

        [Route("changeStatusFriend")]
        [HttpPost]
        public JsonResult ChangeStatusFriend(FriendRequest friendRequest)
        {
            var username = User.Claims.Where(p => p.Type.Equals(ClaimTypes.Name)).FirstOrDefault();

            var data = _listOtherData.ChangeStatusFriend(username.Value.ToString(), friendRequest);

            var remoteIP = Request.HttpContext.Connection.RemoteIpAddress;

            _logging.InsertLogging(new LoggingRequest
            {
                User_type = Consts.USER_TYPE_MEMBER,
                Is_Call_Api = true,
                Api_Name = "/api/app/friendShip/changeStatusFriend",
                Actions = "đổi trạng thái kết bạn",
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
