using CIPlatformIntegration.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatformIntegration.Repository.Interface
{
    public interface IUserRepository
    {
        int userRegistration(User user);

        User userLogin(User _user);
    }
}
