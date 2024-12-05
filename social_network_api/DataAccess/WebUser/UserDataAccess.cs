using social_network_api.Data;
using social_network_api.DataObjects.Request.Authen;
using social_network_api.DataObjects.Request.Common;
using social_network_api.DataObjects.Request.Info;
using social_network_api.DataObjects.Response;
using social_network_api.Helpers;
using social_network_api.Interfaces.Info;
using social_network_api.Models.Authen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace social_network_api.DataAccess.WebUser
{
    public class UserDataAccess : IUser
    {
        private readonly ApplicationDBContext _context;
        private readonly ICommonFunctions _commonFunction;

        public UserDataAccess(ApplicationDBContext context, ICommonFunctions commonFunction)
        {
            _context = context;
            _commonFunction = commonFunction;
        }

        public ApiResponse Delete(DeleteRequest delReq)
        {
            throw new NotImplementedException();
        }

        public ApiResponse Register(RegisterRequest req)
        {

            if (req.Email == null)
            {
                return new ApiResponse("ERROR_MISSING_EMAIL");
            }

            if (req.Username == null)
            {
                return new ApiResponse("ERROR_MISSING_USERNAME");
            }

            if (req.Full_Name == null)
            {
                return new ApiResponse("ERROR_MISSING_FULL_NAME");
            }

            if (req.Password == null)
            {
                return new ApiResponse("ERROR_MISSING_PASSWORD");
            }
            // checkdup username
            var usernameSame = _context.Users.Where(x => x.Username == req.Username).DefaultIfEmpty();
            if (usernameSame != null)
            {
                return new ApiResponse("ERROR_USERNAME_EXIST");
            }

            var emailSame = _context.Users.Where(x => x.Email == req.Email).DefaultIfEmpty();
            if (emailSame != null)
            {
                return new ApiResponse("ERROR_EMAIL_EXIST");
            }
            
            try
            {
                var dataCreate = new User();
                dataCreate.Username = req.Username;
                dataCreate.Address = req.Address;
                dataCreate.Date_Created = DateTime.Now;
                dataCreate.Date_Updated = DateTime.Now;
                dataCreate.Email = req.Email;
                dataCreate.Full_Name = req.Full_Name;
                dataCreate.Password = _commonFunction.ComputeSha256Hash(req.Password);
                dataCreate.Phone = req.Phone;
                dataCreate.Status = 1;
                dataCreate.User_Created = req.Username;
                dataCreate.User_Updated = req.Username;

                _context.Users.Add(dataCreate);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message.ToUpper());
            }
            return new ApiResponse(200);
        }

        public ApiResponse Update(UserRequest req)
        {
            throw new NotImplementedException();
        }
    }
}
