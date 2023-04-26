using CIPlatformIntegration.Entities.Models;
using CIPlatformIntegration.Entities.ViewModel;
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



        HomePageViewModel homePageViewModel(long userIdForFav);
        HomePageViewModel filtering(long userIdForFav,string[]? country, string[]? city, string[]? theme, string[]? skills, string? searchTerm, string? sortValue, int pg);
    }
}
