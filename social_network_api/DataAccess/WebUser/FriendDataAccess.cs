using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using social_network_api.Data;
using social_network_api.DataObjects.Request.Authen;
using social_network_api.DataObjects.Request.Common;
using social_network_api.DataObjects.Response;
using social_network_api.Extensions;
using social_network_api.Helpers;
using social_network_api.Interfaces.AddFriend;
using social_network_api.Models.NetWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace social_network_api.DataAccess.WebUser
{
    public class FriendDataAccess : IFriend
    {

        private readonly ApplicationDBContext _context;
        private readonly ICommonFunctions _commonFunction;

        public FriendDataAccess(ApplicationDBContext context, ICommonFunctions commonFunction)
        {
            _context = context;
            _commonFunction = commonFunction;
        }

        public ApiResponse AddFriend(int idUser, FriendRequest req, string username)
        {
            if (idUser == null)
            {
                return new ApiResponse("ERROR_MISSING_ID_USER");
            }
            
            if (req.id == null)
            {
                return new ApiResponse("ERROR_MISSING_ID_FRIEND");
            }

            var friend = _context.Users.Where(x => x.Id == req.id).FirstOrDefault();
            if (friend == null)
            {
                return new ApiResponse("ERROR_USER_ID_NOT_EXISTS");
            }

            try
            {
                var data = new FriendShip();
                data.Id_Friend = req.id;
                data.Id_User = idUser;
                data.User_Created = username;
                data.Status = req.status;
                data.Date_created = DateTime.Now;
 
                _context.FriendShips.Add(data);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return new ApiResponse("ERROR_ADD_FAIL");
            }

            return new ApiResponse(200);
        }

        public ApiResponse ChangeStatusFriend(string username, FriendRequest request)
        {
            if (username  == null)
            {
                return new ApiResponse("ERROR_MISSING_USERNAME");
            }

            var dataChangeStatus = _context.FriendShips.Where(f => f.Id == request.id).FirstOrDefault();
            if ( request.status == dataChangeStatus.Status )
            {
                return new ApiResponse("ERROR_STATUS_IS_NOT_CHANGE");
            }

            try
            {
                dataChangeStatus.Status = request.status;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                return new ApiResponse(ex.Message);
            }

            return new ApiResponse(200);
        }

        public ApiResponse DeleteFriend(int idUser, DeleteRequest req )
        {
            if (idUser == null)
            {
                return new ApiResponse("ERROR_MISSING_ID_USER");
            }

            if (req.id == null)
            {
                return new ApiResponse("ERROR_MISSING_ID_FRIEND");
            }

            var friendDel = _context.FriendShips.Where(x => x.Id_Friend == req.id && x.Id_User == idUser).FirstOrDefault();
            if (friendDel == null)
            {
                return new ApiResponse("ERROR_FRIEND_NOT_EXISTS");
            }

            try
            {
                _context.FriendShips.Remove(friendDel);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return new ApiResponse("DELETE_FAIL");
            }
            return new ApiResponse(200);
        }

        public ApiResponse GetCountFriend(string username)
        {
            if (username == null) 
            {
                return new ApiResponse("ERROR_MISSING_USERNAME");
            }

            var data = (from fs in _context.FriendShips
                        join u in _context.Users on fs.Id_User equals u.Id
                        where u.Username == username
                        select new
                        {
                            id = fs.Id
                        }
                       ).ToList().Count();

            return new ApiResponse(data);
        }

        public ApiResponse GetList(int idUser , FriendRequest req)
        {
            if (idUser == null)
            {
                return new ApiResponse("ERROR_MISSING_ID_USER");
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

            var listFriend = (from fs in _context.FriendShips
                              join u in _context.Users on fs.Id_User equals u.Id
                              where fs.Id_User == idUser && fs.Status == 2
                              select new
                              {
                                  id = u.Id,
                                  full_name = u.Full_Name,
                                  id_profile = u.Id_Profile,
                                  status = u.Status,
                                  is_Admin = u.Is_Sysadmin,
                                  phone_number = u.Phone,
                                  email = u.Email
                              });

            int countElements = listFriend.Count();

            int totalPage = countElements > 0
                    ? (int)Math.Ceiling(countElements / (double)req.page_size)
                    : 0;

            var dataList = listFriend.Take(req.page_size * req.page_no).Skip(skipElements).ToList();
            var dataResult = new DataListRespone { Page_no = req.page_no, Page_Size = req.page_size, Total_elements = countElements, Total_page = totalPage, Data = dataList };
            return new ApiResponse(dataResult);
        }

        public ApiResponse UpdateStatus(int idUser, FriendRequest req)
        {
            throw new NotImplementedException();
        }


    }
}
