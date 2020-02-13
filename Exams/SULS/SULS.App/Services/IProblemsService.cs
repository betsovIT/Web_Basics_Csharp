using SULS.App.Models;
using SULS.App.ViewModels.Problems;
using SULS.App.ViewModels.Submissions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SULS.App.Services
{
    public interface IProblemsService
    {
        ProblemsViewModel GetAllProblems();

        ProblemInfoModel GetProblemById(string id);

        void Create(string name, int points);

        ProblemWithSubmissionsViewModel GetSubmissions(string problemId);
    }
}
