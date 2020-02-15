using PandaWebApp.Services;
using PandaWebApp.ViewModels.Users;
using SIS.HTTP;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace PandaWebApp.Controllers
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
            var viewModel = usersService.GetUserData(this.User);

            if (viewModel == null)
            {
                return this.View(new UserInfoModel());
            }


            return this.View(viewModel);
        }
    }
}
