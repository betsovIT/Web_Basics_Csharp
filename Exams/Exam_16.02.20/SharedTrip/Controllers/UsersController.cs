using SharedTrip.Services;
using SharedTrip.ViewModels.Users;
using SIS.HTTP;
using SIS.MvcFramework;
using System.ComponentModel.DataAnnotations;

namespace SharedTrip.Controllers
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
        public HttpResponse Login(UserLoginModel input)
        {
            var user = usersService.GetUser(input.Username, input.Password);
            if (user != null)
            {
                this.SignIn(user);

                return this.Redirect("/Trips/All");
            }

            return this.View();
        }

        public HttpResponse Register()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(UserRegistrationInputModel input)
        {
            var emailValidator = new EmailAddressAttribute();

            if (input.Username.Length < 5 || input.Username.Length > 20)
            {
                return Redirect("/Users/Register");
            }
            if (input.Password.Length < 6 || input.Password.Length > 20)
            {
                return Redirect("/Users/Register");
            }
            if (input.Password != input.ConfirmPassword)
            {
                return Redirect("/Users/Register");
            }
            if (usersService.IsUsernameTaken(input.Username))
            {
                return Redirect("/Users/Register");
            }
            if (usersService.IsEmailTaken(input.Email))
            {
                return Redirect("/Users/Register");
            }
            if (!emailValidator.IsValid(input.Email))
            {
                return Redirect("/Users/Register");
            }

            usersService.Register(input.Username, input.Password, input.Email);

            return this.Redirect("/Users/Login");
        }

        public HttpResponse Logout()
        {
            this.SignOut();

            return this.Redirect("/");
        }
    }
}
