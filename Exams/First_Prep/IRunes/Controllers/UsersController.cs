using IRunes.Services;
using IRunes.ViewModels.Users;
using SIS.HTTP;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace IRunes.Controllers
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
        public HttpResponse Login(LoginInputModel input)
        {
            var userId = usersService.GetUserId(input.Username, input.Password);
            if (userId != null)
            {
                this.SignIn(userId);
                return this.Redirect("/");
            }

            return this.Redirect("/Users/Login");
        }

        public HttpResponse Register()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputModel input)
        {
            if (string.IsNullOrWhiteSpace(input.Email))
            {
                return this.Error("Email can not be empty!");
            }

            if (input.Password.Length < 6 || input.Password.Length > 20)
            {
                return this.Error("Passwer must be at least 6 character and at most - 20.");
            }

            if (input.Username.Length < 4 || input.Username.Length > 10)
            {
                return this.Error("Username must be at least 4 character and at most - 10.");
            }

            if (input.Password != input.ConfirmPassword)
            {
                return this.Error("Passwords do not match.");
            }

            if (this.usersService.EmailExists(input.Email))
            {
                return this.Error("Email already exists.");
            }

            if (this.usersService.UsernameExists(input.Username))
            {
                return this.Error("Username already exists.");
            }

            this.usersService.Register(input.Username, input.Email, input.Password);

            return Redirect("/Users/Login");
        }

        public HttpResponse Logout()
        {
            this.SignOut();

            return Redirect("/");
        }
    }
}
