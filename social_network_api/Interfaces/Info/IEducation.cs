using social_network_api.DataObjects.Request.Authen;
using social_network_api.DataObjects.Request.Common;
using social_network_api.DataObjects.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace social_network_api.Interfaces.Info
{
    public interface IEducation
    {
        public ApiResponse Create(string username, EducationRequest req);
        public ApiResponse Update(string username, EducationRequest req);
        public ApiResponse Delete(string username, DeleteRequest req);
        public ApiResponse GetAll(string username, EducationRequest req);
        public ApiResponse GetDetail(string username, int idEducation);
    }
}
