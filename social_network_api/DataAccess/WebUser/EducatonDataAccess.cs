using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using social_network_api.Data;
using social_network_api.DataObjects.CommentRequest;
using social_network_api.DataObjects.Request.Authen;
using social_network_api.DataObjects.Request.Common;
using social_network_api.DataObjects.Response;
using social_network_api.Extensions;
using social_network_api.Interfaces.Info;
using social_network_api.Models.Authen;
using social_network_api.Models.Info;
using System;
using System.Diagnostics;
using System.Linq;
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
            if (username == null)
            {
                return new ApiResponse("ERROR_MISSING_USERNAME");
            }

            var dataUser = _context.Users.Where(e => e.Username == username).FirstOrDefault();
            if (dataUser == null)
            {
                return new ApiResponse("ERROR_USERNAME_NOT_EXISTS");
            }
            
            var dataDel = _context.Educations.Where(e => e.Id_User == dataUser.Id && e.Id == req.id).FirstOrDefault();

            if (dataDel == null) 
            {
                return new ApiResponse("ERROR_EDUCATION_NOT_EXISTS");
            }

            try
            {
                _context.Educations.Remove(dataDel);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message.ToString());
            }

            return new ApiResponse(200);
        }

        public ApiResponse GetAll(string username, EducationRequest req)
        {
            if (username == null)
            {
                return new ApiResponse("ERROR_MISSING_USERNAME");
            }

            // check exists username 
            var dataUser = _context.Users.Where(e => e.Username == username).FirstOrDefault();
            if (dataUser == null)
            {
                return new ApiResponse("ERROR_USERNAME_NOT_EXISTS");
            }
            // Default page_no, page_size
            if (req.page_size < 1)
            {
                req.page_size = Consts.PAGE_SIZE;
            }

            if (req.page_no < 1)
            {
                req.page_no = 1;
            }
            // Số lượng Skip
            int skipElements = (req.page_no - 1) * req.page_size;
            var listComment = (from e in _context.Educations 
                               join ue in _context.UserEducations on e.Id_User equals ue.Id_User
                               where ue.Id_User == dataUser.Id
                               select new
                               {
                                   id = e.Id,
                                   name = e.Name,
                                   code = e.Code,
                                   degree = ue.Degree,
                                   from_year = ue.From_Year,
                                   to_year = ue.To_Year,
                                   is_Graduate = ue.Is_Graduate
                               }
                               ).ToList();

            int countElements = listComment.Count();

            int totalPage = countElements > 0
                    ? (int)Math.Ceiling(countElements / (double)req.page_size)
            : 0;

            var dataList = listComment.Take(req.page_size * req.page_no).Skip(skipElements).ToList();
            var dataResult = new DataListRespone { Page_no = req.page_no, Page_Size = req.page_size, Total_elements = countElements, Total_page = totalPage, Data = dataList };
            return new ApiResponse(dataResult);
    
        }

        public ApiResponse GetDetail(string username, int idEducation)
        {
            if (username == null) 
            {
                return new ApiResponse("ERROR_MISSING_USERNAME"); 
            }

            if (idEducation == null)
            {
                return new ApiResponse("ERROR_MISSING_IDEDUCATION");
            }

            
            var dataUser = _context.Users.Where(e => e.Username == username).FirstOrDefault();
            if (dataUser == null)
            {
                return new ApiResponse("ERROR_USERNAME_NOT_EXISTS");
            }

            var dataResult = (from e in _context.Educations
                             join ue in _context.UserEducations on e.Id_User equals ue.Id_User
                             where ue.Id_User == dataUser.Id & ue.Id == idEducation
                             select new
                             {
                                 id = e.Id,
                                 name = e.Name,
                                 code = e.Code,
                                 degree = ue.Degree,
                                 from_year = ue.From_Year,
                                 to_year = ue.To_Year,
                                 is_Graduate = ue.Is_Graduate
                             }).FirstOrDefault();
            if (dataResult == null) 
            {
                return new ApiResponse("ERROR_ID_EDUCATION_NOT_EXISTS");
            }

            return new ApiResponse(dataResult);

        }

        public ApiResponse Update(string username, EducationRequest req)
        {
            if (username == null)
            {
                return new ApiResponse("ERROR_MISSING_USERNAME");
            }

            var dataUser = _context.Users.Where(e => e.Username == username).FirstOrDefault();
            if (dataUser == null)
            {
                return new ApiResponse("ERROR_USERNAME_NOT_EXISTS");
            }

            // check validate req

            // end check 

            using (var transaction = _context.Database.BeginTransaction()) 
            {
                try
                {
                    // add education 
                    var dataEducation = new Education();
                    dataEducation.Name = req.Name;
                    dataEducation.Code = req.Code;
                    dataEducation.Id_User = dataUser.Id;
                    dataEducation.EstablishedYear = req.EstablishedYear;
                    dataEducation.Date_Created = DateTime.Now;
                    dataEducation.Date_Updated = DateTime.Now;
                    dataEducation.User_Created = username;
                    dataEducation.User_Updated = username;

                    _context.Educations.Add(dataEducation);
                    _context.SaveChanges();

                    // add user education 
                    var dataUserEdu = new UserEducation();
                    dataUserEdu.Status = 1;
                    dataUserEdu.Degree = req.Degree;
                    dataUserEdu.Id_User = dataUser.Id;
                    dataUserEdu.From_Year = req.From_Year;
                    dataUserEdu.To_Year = req.To_Year;
                    dataUserEdu.Is_Graduate = req.Is_Graduate;
                    dataUserEdu.Date_Created = DateTime.Now;
                    dataUserEdu.Date_Updated = DateTime.Now;
                    dataUserEdu.User_Created = username;
                    dataUserEdu.User_Updated = username;

                    _context.UserEducations.Add(dataUserEdu);
                    _context.SaveChanges();
                    //
                    transaction.Commit();
                    transaction.Dispose();
                    return new ApiResponse(200);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    transaction.Dispose();

                    return new ApiResponse(ex.Message.ToString());
                }
                
            }
        }


    }
}
