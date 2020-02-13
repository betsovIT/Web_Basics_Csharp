namespace SULS.App
{
    using Microsoft.EntityFrameworkCore;
    using SIS.HTTP;
    using SIS.MvcFramework;
    using SULS.App.Data;
    using SULS.App.Services;
    using System.Collections.Generic;

    public class StartUp : IMvcApplication
    {
        public void Configure(IList<Route> routeTable)
        {
            var db = new ApplicationDbContext();
            db.Database.Migrate();
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUsersService, UsersService>();
            serviceCollection.Add<IProblemsService, ProblemService>();
            serviceCollection.Add<ISubmissionsService, SubmissionService>();
        }
    }
}