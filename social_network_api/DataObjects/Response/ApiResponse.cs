using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace social_network_api.DataObjects.Response
{
    public class ApiResponse
    {
        public string Code { get; set; }
        public object Data { get; set; }
        public string Error { get; set; }

        public ApiResponse(int status)
        {
            switch (status)
            {
                case 200:
                    this.Code = "200";
                    break;
                case 400:
                    this.Error = "BAD_REQUEST";
                    this.Code = "400";
                    break;
                case 404:
                    this.Error = "NO_Data_FOUND";
                    this.Code = "400";
                    break;
                default:
                    this.Code = "400";
                    break;
            }
        }

        public ApiResponse(object Data)
        {
            this.Code = "200";
            this.Data = Data;
            this.Error = null;
        }

        public ApiResponse(int status, string Data)
        {
            this.Code = status.ToString();
            this.Data = Data;
            this.Error = null;
        }

        public ApiResponse(int status, object Data)
        {
            this.Code = status.ToString();
            this.Data = Data;
            this.Error = null;
        }

        public ApiResponse(string Error)
        {
            this.Code = "400";
            this.Error = Error;
            this.Data = null;
        }
    }
}
