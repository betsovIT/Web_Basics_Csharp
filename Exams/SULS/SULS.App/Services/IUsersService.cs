using System;
using System.Collections.Generic;
using System.Text;

namespace SULS.App.Services
{
    public interface IUsersService
    {
        void Register(string username, string password, string email);

        bool EmailExists(string email);

        bool UsernameExists(string username);

        string GetUserId(string username, string password);
    }
}
