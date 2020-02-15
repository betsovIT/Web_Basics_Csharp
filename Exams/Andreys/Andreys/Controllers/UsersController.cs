using Andreys.Services;
using Andreys.ViewModels;
using Andreys.ViewModels.Users;
using SIS.HTTP;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Andreys.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public HttpResponse Login()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(UsersLoginInputModel input)
        {
            var userId = usersService.GetUserId(input.Username, input.Password);

            if (userId != null)
            {
                this.SignIn(userId);
            }

            return this.Redirect("/");
        }

        public HttpResponse Register()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(UserRegistrationInputModel input)
        {
            if (usersService.IsUsernameTaken(input.Username))
            {
                return this.Error("Username is already taken.");
            }
            if (usersService.IsEmailTaken(input.Email))
            {
                return this.Error("Email is already taken");
            }
            if (input.Username.Length < 4 || input.Username.Length > 10)
            {
                return this.Error("Username must be between 4 and 10 characters.");
            }
            if (input.Password.Length < 6 || input.Password.Length > 20)
            {
                return this.Error("Password must be between 6 and 20 characters.");
            }
            if (input.Password != input.ConfirmPassword)
            {
                return this.Error("Passwords do not match.");
            }

            usersService.Register(input.Username, input.Password, input.Email);

            return this.Redirect("/Users/Login");
        }
    }
}
