using social_network_api.Data;
using social_network_api.DataObjects.Request.Authen;
using social_network_api.DataObjects.Response;
using social_network_api.Interfaces.Info;
using social_network_api.Models.Authen;
using social_network_api.Models.Info;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace social_network_api.DataAccess.WebUser
{
    public class ProfileDataAccess : IProfile
    {
        private readonly ApplicationDBContext _context;

        public ProfileDataAccess(ApplicationDBContext context)
        {
            _context = context;
        }

        public ApiResponse Create(string user, ProfileRequest req)
        {
            if (user == null) 
            {
                return new ApiResponse("ERROR_MISSING_USER");
            }

            if (req.Address == null) 
            {
                return new ApiResponse("ERROR_MISSING_ADDRESS");
            }

            if (req.Avatar == null)
            {
                return new ApiResponse("ERROR_MISSING_AVATAR");
            }

            if (req.CoverPhoto == null)
            {
                return new ApiResponse("ERROR_MISSING_COVERPHOTO");
            }
            var data = new Profile();
            try
            {
                data.Address = req.Address;
                data.Avatar = req.Avatar;
                data.CoverPhoto = req.CoverPhoto;
                data.Date_Created = DateTime.Now;
                data.Date_Updated = DateTime.Now;
                data.User_Created = user;
                data.User_Updated = user;

                _context.Profiles.Add(data);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message.ToString());
            }
            
            return new ApiResponse(data);
        }

        public ApiResponse Delete(string user)
        {
            if (user == null)
            {
                return new ApiResponse("ERROR_MISSING_USER");
            }

            var dataDel = _context.Users.Where(e => e.Username == user).FirstOrDefault(); 

            if (dataDel == null)
            {
                return new ApiResponse("USERNAME_NOT_EXISTS");
            }

            //find profile 

            var profileDel = _context.Profiles.Where(e => e.Id == dataDel.Id_Profile).FirstOrDefault();
            if (profileDel == null)
            {
                return new ApiResponse("PROFILE_NOT_EXISTS");
            }

            _context.Profiles.Remove(profileDel);
            _context.SaveChanges();
            return new ApiResponse(200);
        }

        public ApiResponse Get(string user)
        {
            if (user == null)
            {
                return new ApiResponse("ERROR_MISSING_USER");
            }

            var data = (from p in _context.Profiles
                        join u in _context.Users on p.Id equals u.Id_Profile
                        where u.Username == user
                        select new
                        {
                            Avatar = p.Avatar,
                            CoverPhoto = p.CoverPhoto,
                            Address = p.Address,
                            Date_Created = p.Date_Created,
                            User_Created = p.User_Created,
                            Date_Updated = p.Date_Updated,
                            User_Updated = p.User_Updated,
                        }
                        );

            return new ApiResponse( data );

        }

        public ApiResponse Update(string user, ProfileRequest req)
        {
            if (user == null)
            {
                return new ApiResponse("ERROR_MISSING_USER");
            }

            // check exists user 
            var userData = _context.Users.Where(e => e.Username == user).FirstOrDefault();
            if (userData == null)
            {
                return new ApiResponse("ERROR_USERNAME_NOT_EXISTS");
            }

            // check profile 

            var profile = _context.Profiles.Where(p => p.Id == userData.Id_Profile).FirstOrDefault();
            if (profile == null) 
            {
                return new ApiResponse("ERROR_PROFILE_NOT_EXISTS");
            }

            try
            {
                profile.CoverPhoto = req.CoverPhoto;
                profile.Address = req.Address;
                profile.Avatar = req.Avatar;

                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                return new ApiResponse(ex.Message.ToString());
            }

            return new ApiResponse(200);
        }
    }
}
