using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using social_network_api.Data;
using social_network_api.DataObjects.Response;
using social_network_api.Helpers;
using social_network_api.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace social_network_api.Controllers
{
    [Route("testQuery")]
    [ApiController]
    public class TestResult : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly ILoggingHelpers _loggin;
        private readonly ICommonFunctions _commonFuntions;
        private readonly IJwt _jwtAuthen;

        public TestResult(ApplicationDBContext context, ILoggingHelpers loggin, ICommonFunctions commonFuntions, IJwt jwtAuthen)
        {
            _context = context;
            _loggin = loggin;
            _commonFuntions = commonFuntions;
            _jwtAuthen = jwtAuthen;
        }
        [AllowAnonymous]
        [Route("test")]
        [HttpGet]
        public JsonResult GetListUser()
        {

            var resultTest = (from p in _context.Users select new  { p.Email  }).FirstOrDefault();
                             
            return new JsonResult(new ApiResponse(resultTest)) { StatusCode = 200 };
        }
    }
}
