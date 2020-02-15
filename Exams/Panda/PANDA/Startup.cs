namespace PandaWebApp
{
    using Microsoft.EntityFrameworkCore;
    using PandaWebApp.Data;
    using PandaWebApp.Services;
    using SIS.HTTP;
    using SIS.MvcFramework;
    using System.Collections.Generic;

    public class Startup : IMvcApplication
    {
        public void Configure(IList<Route> routeTable)
        {
            var db = new ApplicationDbContext();
            db.Database.Migrate();
        }

        public void ConfigureServices(IServiceCollection collection)
        {
            collection.Add<IUsersService, UsersService>();
        }
    }
}
