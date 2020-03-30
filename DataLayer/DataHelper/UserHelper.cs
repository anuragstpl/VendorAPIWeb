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
        public bool LoginUser(UserEntity userEntity)
        {
            bool isUserValid = false;
            using (uow = new VendorAPIRepository())
            {
                isUserValid = uow.UserRepository.Get().Any(x => x.Username == userEntity.Username && x.Password == userEntity.Password);
            }
            return isUserValid;
        }
    }
}
