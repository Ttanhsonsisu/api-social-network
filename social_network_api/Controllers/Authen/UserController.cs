using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using social_network_api.Data;
using social_network_api.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace social_network_api.Controllers.Authen
{
    
    [ApiController]
    [Authorize(Policy = "AppUser")]
    [Route("/api/user")]
    public class UserController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly ICommonFunctions _common;
        private readonly ILoggingHelpers _logging;
        public UserController(ApplicationDBContext context, ICommonFunctions common, ILoggingHelpers logging)
        {
            _context = context;
            _common = common;
            _logging = logging;
        }


    }
}
