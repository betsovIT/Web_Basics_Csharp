using SIS.HTTP;
using SIS.MvcFramework;
using SULS.App.Services;
using SULS.App.ViewModels.Submissions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SULS.App.Controllers
{
    public class SubmissionsController : Controller
    {
        private readonly IProblemsService problemService;
        private readonly ISubmissionsService submissionService;

        public SubmissionsController(IProblemsService problemService, ISubmissionsService submissionService)
        {
            this.problemService = problemService;
            this.submissionService = submissionService;
        }

        public HttpResponse Create(string id)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModel = problemService.GetProblemById(id);
            return this.View(viewModel);
        }

        [HttpPost]
        public HttpResponse Create(CreateSubmissionInputModel input)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (input.Code.Length < 30 && input.Code.Length > 800)
            {
                return this.Error("The submission code must be between 30 and 800 characters");
            }

            submissionService.Create(input.ProblemId, this.User, input.Code);

            return this.Redirect("/");
        }
    }
}
