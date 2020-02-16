using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.Services
{
    public interface IUsersService
    {
        void Register(string username, string password, string email);

        bool IsUsernameTaken(string username);

        bool IsEmailTaken(string email);

        string GetUser(string username, string password);
    }
}
