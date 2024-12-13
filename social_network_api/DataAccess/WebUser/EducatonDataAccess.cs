using social_network_api.Data;
using social_network_api.DataObjects.Request.Authen;
using social_network_api.DataObjects.Request.Common;
using social_network_api.DataObjects.Response;
using social_network_api.Interfaces.Info;
using social_network_api.Models.Info;
using System;
using System.Threading;

namespace social_network_api.DataAccess.WebUser
{
    public class EducatonDataAccess : IEducation
    {
        private readonly ApplicationDBContext _context;

        public EducatonDataAccess(ApplicationDBContext context)
        {
            _context = context;
        }

        public ApiResponse Create(string username, EducationRequest req)
        {
            if (username == null )
            {
                return new ApiResponse("ERROR_MISSING_USERNAME");
            }
            // validate req

            var data = new Education();
            try
            {
                data.Name = req.Name;
                
                data.User_Created = username;
                data.User_Updated = username;
                data.Date_Created = DateTime.Now;
                data.Date_Updated = DateTime.Now;
                data.Code = req.Code;
                data.Description = req.Description;
                data.EstablishedYear = req.EstablishedYear;

                _context.Educations.Add(data);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }

            return new ApiResponse(data);
        }

        public ApiResponse Delete(string username, DeleteRequest req)
        {
            throw new System.NotImplementedException();
        }

        public ApiResponse GetAll(string username, EducationRequest req)
        {
            throw new System.NotImplementedException();
        }

        public ApiResponse GetDetail(string username, int idEducation)
        {
            throw new System.NotImplementedException();
        }

        public ApiResponse Update(string username, EducationRequest req)
        {
            throw new System.NotImplementedException();
        }
    }
}
