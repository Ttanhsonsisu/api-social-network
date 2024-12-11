using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using social_network_api.Data;
using social_network_api.DataObjects.CommentRequest;
using social_network_api.DataObjects.Common;
using social_network_api.Extensions;
using social_network_api.Helpers;
using social_network_api.Interfaces.Info;
using System.Linq;
using System.Security.Claims;

namespace social_network_api.Controllers.Authen
{
    [ApiController]
    [Route("/api/profile")]
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

        [Route("create")]
        [HttpPost]
        public JsonResult Create(CommentRequest commentRequest)
        {
            var username = User.Claims.Where(p => p.Type.Equals(ClaimTypes.Name)).FirstOrDefault();
            
            //var user = _context.Users.Where(u => u.Username == username.Value).FirstOrDefault();

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


    }
}
