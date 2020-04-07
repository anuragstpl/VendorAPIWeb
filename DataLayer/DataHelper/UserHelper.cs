using DataLayer.UnitOfWork;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataHelper
{
    public class UserHelper
    {
        VendorAPIRepository uow = null;
        public UserEntity LoginUser(UserEntity userEntity)
        {
            using (uow = new VendorAPIRepository())
            {
                //isUserValid = uow.UserRepository.Get().Any(x => x.Username == userEntity.Username && x.Password == userEntity.Password);
                userEntity = uow.UserRepository.Get().Where(urp => urp.Username == userEntity.Username && CommonHelper.Decrypt(urp.Password) == userEntity.Password)
                    .Select(x => new UserEntity
                    {
                        UserID = Convert.ToInt32(x.UserID),
                        Username = x.Username,
                        Password = x.Password,
                        UserRoleID = Convert.ToInt32(x.UserRoleID),
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Email = x.Email,
                        PhoneNo = x.PhoneNo
                    }).FirstOrDefault();
            }
            return userEntity;
        }

        public UserEntity GetProfileData(int userId)
        {
            UserEntity userEntity = new UserEntity();
            using (uow = new VendorAPIRepository())
            {
                userEntity = uow.UserRepository.Get().Where(z => z.UserID == userId).Select(x => new UserEntity
                {
                    UserID = userId,
                    UserRoleID = Convert.ToInt32(x.UserRoleID),
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    PhoneNo = x.PhoneNo
                }).FirstOrDefault();
            }
            return userEntity;
        }

        public UserProfileData GetUserProfileData(int userId)
        {
            UserProfileData userProfileData = new UserProfileData();

            using (uow = new VendorAPIRepository())
            {
                UserEntity userEntity = GetProfileData(userId);
                try
                {
                    userProfileData.UserId = userId;
                    userProfileData.UserRoleId = userEntity.UserRoleID;
                    userProfileData.FirstName = userEntity.FirstName;
                    userProfileData.LastName = userEntity.LastName;
                    userProfileData.Email = userEntity.Email;
                    userProfileData.PhoneNo = userEntity.PhoneNo;
                }
                catch (Exception ex)
                {
                }
            }
            return userProfileData;
        }
        public void UpdateUser(UserProfileData userProfileData)
        {
            try
            {
                using (uow = new VendorAPIRepository())
                {
                    User user = uow.UserRepository.Get().Where(x => x.UserID == userProfileData.UserId).FirstOrDefault();
                    if (user != null)
                    {
                        user.FirstName = userProfileData.FirstName;
                        user.LastName = userProfileData.LastName;
                        user.Email = userProfileData.Email;
                        user.PhoneNo = userProfileData.PhoneNo;
                        uow.UserRepository.Update(user);
                        uow.Save();
                    }
                }
            }
            catch (Exception ex) { }
        }

        public void UpdateEmail(UserProfileData userProfileData)
        {
            try
            {
                using (uow = new VendorAPIRepository())
                {
                    User user = uow.UserRepository.Get().Where(x => x.UserID == userProfileData.UserId).FirstOrDefault();
                    if (user != null)
                    {
                        user.Email = userProfileData.Email;
                        uow.UserRepository.Update(user);
                        uow.Save();
                    }
                }
            }
            catch (Exception ex) { }
        }

        public UserEntity GetPassword(UserEntity userEntity)
        {
            using (uow = new VendorAPIRepository())
            {
                userEntity = uow.UserRepository.Get().Where(urp => urp.Email == userEntity.Email)
                    .Select(x => new UserEntity
                    {
                        UserID = Convert.ToInt32(x.UserID),
                        Username = x.Username,
                        Password = x.Password,
                        UserRoleID = Convert.ToInt32(x.UserRoleID),
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Email = x.Email,
                        PhoneNo = x.PhoneNo
                    }).FirstOrDefault();
            }
            return userEntity;
        }
    }
}
