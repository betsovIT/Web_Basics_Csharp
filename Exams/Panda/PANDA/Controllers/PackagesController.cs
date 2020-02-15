using SIS.HTTP;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace PandaWebApp.Controllers
{
    public class PackagesController : Controller
    {
        public HttpResponse Details(string id)
        {
            return this.View();
        }
    }
}
