using SIS.HTTP;
using SIS.MvcFramework;
using SULS.App.Services;

namespace SULS.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProblemsService service;

        public HomeController(IProblemsService service)
        {
            this.service = service;
        }
        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (this.IsUserLoggedIn())
            {
                var problmesViewModel = service.GetAllProblems();
                return this.View(problmesViewModel, "IndexLoggedIn");
            }

            return this.View();
        }
    }
}