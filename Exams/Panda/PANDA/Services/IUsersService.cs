using PandaWebApp.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace PandaWebApp.Services
{
    public interface IUsersService
    {
        void Register(string username, string password, string email);

        bool AnyRegisteredUsers();

        bool UsernameExists(string username);

        bool EmailExists(string email);

        string GetUserId(string username, string password);

        UserInfoModel GetUserData(string id);
    }
}
