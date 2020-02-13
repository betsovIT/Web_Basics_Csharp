using SIS.HTTP;
using SIS.MvcFramework;
using SULS.App.Services;
using SULS.App.ViewModels.Problems;
using System;
using System.Collections.Generic;
using System.Text;

namespace SULS.App.Controllers
{
    public class ProblemsController : Controller
    {
        private readonly IProblemsService problemsService;

        public ProblemsController(IProblemsService service)
        {
            this.problemsService = service;
        }

        public HttpResponse Create()
        {
            if (!this.IsUserLoggedIn())
            {
                return Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(ProblemCreateInputModel input)
        {
            if (input.Name.Length < 5 && input.Name.Length > 20)
            {
                return this.Error("Problem's name length must be between 5 and 20 characters.");
            }

            if (input.Points < 50 || input.Points > 300)
            {
                return this.Error("A problem should award between 50 and 300 points.");
            }

            problemsService.Create(input.Name, input.Points);

            return Redirect("/");
        }

        public HttpResponse Details(string id)
        {
            var viewModel = problemsService.GetSubmissions(id);
            return this.View(viewModel);
        }
    }
}
