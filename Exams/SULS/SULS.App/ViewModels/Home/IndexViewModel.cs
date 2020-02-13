using SULS.App.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SULS.App.ViewModels.Home
{
    public class IndexViewModel
    {
        public IEnumerable<Problem> Problems { get; set; }
    }
}