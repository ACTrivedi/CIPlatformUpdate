using CIPlatformIntegration.Entities.Data;
using CIPlatformIntegration.Entities.Models;
using CIPlatformIntegration.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatformIntegration.Repository.Repository
{
    public class UserRepository: IUserRepository
    {
        private readonly CIDatabaseContext _cidatabaseContext;

        public UserRepository(CIDatabaseContext cidatabaseContext)
        {
            _cidatabaseContext = cidatabaseContext;
        }

       

        public int userRegistration(User user)
        {
 
            var useremailverify = _cidatabaseContext.Users.FirstOrDefault(u => u.Email == user.Email);
            if (useremailverify != null)
            {
                return 3;
                
            }
            else 
            {
                User data = new User();
                data.Email = user.Email;
                if (user.Password != null)
                {
                    data.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                }
                

                data.PhoneNumber = user.PhoneNumber;
                data.CityId = 1;
                data.CountryId = 1;
                data.LastName = user.LastName;
                data.FirstName = user.FirstName;

                if (data.FirstName != null && data.LastName != null && data.PhoneNumber != null && data.Email != null && data.Password != null)
                {
                    _cidatabaseContext.Users.Add(data);
                    _cidatabaseContext.SaveChanges();
                    return 1;
                }
                else
                {
                    return 2;
                }

                
            }


        }

        public User userLogin(User _user)
        {
            if (_user.Email != null && _user.Password != null)
            {
                var emailstatus = _cidatabaseContext.Users.FirstOrDefault(m => m.Email == _user.Email);

                bool verified = BCrypt.Net.BCrypt.Verify(_user.Password, emailstatus.Password);
                var status = _cidatabaseContext.Users.Where(m => m.Email == _user.Email && verified).FirstOrDefault();
                if (status == null)
                {
                    return null;
                }
                else
                {
                    return status;
                }
            }
            return null;
        }
    }
}

