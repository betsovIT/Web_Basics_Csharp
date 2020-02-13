using SULS.App.ViewModels.Submissions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SULS.App.ViewModels.Problems
{
    public class ProblemWithSubmissionsViewModel
    {
        public string Name { get; set; }

        public int MaxPoints { get; set; }

        public IEnumerable<SubmissionInfoViewModel> Submissions { get; set; }
    }
}
