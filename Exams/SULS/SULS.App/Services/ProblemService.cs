using SULS.App.Data;
using SULS.App.Models;
using SULS.App.ViewModels.Problems;
using SULS.App.ViewModels.Submissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SULS.App.Services
{
    public class ProblemService : IProblemsService
    {
        private readonly ApplicationDbContext db;

        public ProblemService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void Create(string name, int points)
        {
            var problem = new Problem
            {
                Name = name,
                Points = points
            };

            this.db.Problems.Add(problem);
            db.SaveChanges();
        }

        public ProblemsViewModel GetAllProblems()
        {
            var problems = db.Problems.Select(p => new ProblemInfoModel
            {
                Id = p.Id,
                Name = p.Name,
                SubmissionCount = p.Submissions.Count
            }).ToList();

            return new ProblemsViewModel
            {
                Problems = problems
            };
        }

        public ProblemInfoModel GetProblemById(string id)
        {
            return db.Problems.Where(p => p.Id == id).Select(p => new ProblemInfoModel
            {
                Id = p.Id,
                Name = p.Name,
                SubmissionCount = p.Submissions.Count
            }).FirstOrDefault();
        }

        public ProblemWithSubmissionsViewModel GetSubmissions(string problemId)
        {
            var problem = db.Problems.FirstOrDefault(x => x.Id == problemId);
            var submissions = db.Submissions.Where(x => x.ProblemId == problemId).ToList();

            var result = new ProblemWithSubmissionsViewModel
            {
                Name = problem.Name,
                MaxPoints = problem.Points,
                Submissions = submissions.Select(x => new SubmissionInfoViewModel
                {
                    Id = x.Id,
                    AchievedResult = x.AchievedResult,
                    User = db.Users.FirstOrDefault(u => u.Id == x.UserId).Username,
                    CreatedOn = x.CreatedOn.ToString("dd/MM/yyyy")
                })
            };

            return result;
        }
    }
}
