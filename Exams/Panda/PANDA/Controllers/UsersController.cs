using PandaWebApp.Services;
using PandaWebApp.ViewModels.Users;
using SIS.HTTP;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PandaWebApp.Controllers
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

        public HttpResponse Logout()
        {
            this.SignOut();

            return this.Redirect("/");
        }

        [HttpPost]
        public HttpResponse Login(UserLoginModel input)
        {
            var id = usersService.GetUserId(input.Username, input.Password);

            if (id != null)
            {
                this.SignIn(id);
            }

            return this.Redirect("/");
        }

        public HttpResponse Register()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(UserInputViewModel input)
        {
            var emailValidator = new EmailAddressAttribute();

            if (input.Username.Length < 5 || input.Username.Length > 20)
            {
                return this.Error("Username must be between 5 and 20 characters.");
            }
            if (input.Password.Length<5 || input.Password.Length > 20)
            {
                return this.Error("Password must be between 5 and 20 characters.");
            }
            if (!emailValidator.IsValid(input.Email))
            {
                return this.Error("Not a valid email address.");
            }
            if (usersService.UsernameExists(input.Username))
            {
                return this.Error("Username is already taken.");
            }
            if (usersService.EmailExists(input.Email))
            {
                return this.Error("Email is already taken.");
            }
            if (input.Password != input.ConfirmPassword)
            {
                return this.Error("Passwords do not match");
            }

            usersService.Register(input.Username, input.Password, input.Email);

            return this.Redirect("/Users/Login");
        }

    }
}
