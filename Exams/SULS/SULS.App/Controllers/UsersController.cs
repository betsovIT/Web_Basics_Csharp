using SIS.HTTP;
using SIS.MvcFramework;
using SULS.App.Services;
using SULS.App.ViewModels.Users;
using System.ComponentModel.DataAnnotations;

namespace SULS.App.Controllers
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

        public HttpResponse Register()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputModel input)
        {
            var emailValidator = new EmailAddressAttribute();

            if (input.Username.Length < 5 || input.Username.Length > 20)
            {
                return this.Error("Username can not be under 5 or over 20 characters");
            }
            if (input.Password.Length < 6 || input.Password.Length > 20)
            {
                return this.Error("Password must be between 6 and 20 characters.");
            }
            if (input.Password != input.ConfirmPassword)
            {
                return this.Error("Passwords do not match.");
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
                return this.Error("Email is already taken");
            }

            this.usersService.Register(input.Username, input.Email, input.Password);

            return Redirect("/Users/Login");

        }

        [HttpPost]
        public HttpResponse Login(LoginInputModel input)
        {
            string userId = usersService.GetUserId(input.Username, input.Password);

            if (userId != null)
            {
                this.SignIn(userId);
                return Redirect("/");
            }

            return Redirect("/Users/Login");
        }

        [HttpGet]
        public HttpResponse Logout()
        {
            this.SignOut();

            return this.Redirect("/");
        }
    }
}