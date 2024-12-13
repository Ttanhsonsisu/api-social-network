using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using social_network_api.Data;
using social_network_api.DataObjects.Response;
using social_network_api.Helpers;
using social_network_api.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net.Http;
using System.Text;
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
        private readonly HttpClient _httpClient;

        public TestResult(ApplicationDBContext context, ILoggingHelpers loggin, ICommonFunctions commonFuntions, IJwt jwtAuthen, HttpClient client)
        {
            _context = context;
            _loggin = loggin;
            _commonFuntions = commonFuntions;
            _jwtAuthen = jwtAuthen;
            _httpClient = client;

        }
        [AllowAnonymous]
        [Route("test")]
        [HttpGet]
        public async Task<JsonResult> TestApi()
        {
            string url = "https://fakestoreapi.com/products";
            
            var response = await _httpClient.GetAsync(url);
            response .EnsureSuccessStatusCode();

            var data = await response.Content.ReadAsStringAsync();
            
            var dataResult = JsonConvert.DeserializeObject<List<Object>>(data);
            return new JsonResult(new ApiResponse(dataResult)) { StatusCode = 200 };
        }

        public class testParam
        {
            public string Title { get; set; }
            public double price { get; set; }
            public string description { get; set; }
            public string image { get; set; }
            public string category { get; set; }
        }

        [AllowAnonymous]
        [Route("testparam")]
        [HttpPost]
        public async Task<JsonResult> TestParam(testParam body)
        {
            string url = "https://fakestoreapi.com/products";

            var request = new HttpRequestMessage(HttpMethod.Post, url);


            var jsonBody = JsonConvert.SerializeObject(body);
            request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();

            var dataResult = JsonConvert.SerializeObject(responseContent);

            return new JsonResult(new ApiResponse(dataResult)) { StatusCode = 200 };
        }
    }
}
