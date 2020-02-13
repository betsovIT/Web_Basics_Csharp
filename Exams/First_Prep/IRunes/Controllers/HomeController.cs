using IRunes.Services;
using IRunes.ViewModels.Home;
using SIS.HTTP;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace IRunes.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUsersService usersService;

        public HomeController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [HttpGet("/")]
        [HttpGet("/Home/Index")]
        public HttpResponse Index()
        {
            if (this.IsUserLoggedIn())
            {
                var viewModel = new IndexViewModel();
                viewModel.Username = this.usersService.GetUserName(this.User);
                return this.View(viewModel, "Home" );
            }

            return this.View();
        }
    }
}
