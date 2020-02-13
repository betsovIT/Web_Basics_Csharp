using SULS.App.Data;
using SULS.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SULS.App.Services
{
    public class SubmissionService : ISubmissionsService
    {
        private readonly ApplicationDbContext db;

        public SubmissionService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Create(string problemId, string userId, string code)
        {
            var problemMaxScore = db.Problems.FirstOrDefault(p => p.Id == problemId).Points;

            var submission = new Submission
            {
                UserId = userId,
                ProblemId = problemId,
                Code = code,
                CreatedOn = DateTime.UtcNow,
                AchievedResult = GetRandomResult(problemMaxScore)
            };

            db.Submissions.Add(submission);
            db.SaveChanges();
        }

        private int GetRandomResult(int maxScore)
        {
            Random rnd = new Random();
            int result = rnd.Next(0, maxScore);

            return result;
        }
    }
}
