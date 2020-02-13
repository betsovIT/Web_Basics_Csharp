using SIS.HTTP;
using SIS.HTTP.Response;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var routeTable = new List<Route>();
            routeTable.Add(new Route(HttpMethodType.Get, "/", Index));
            routeTable.Add(new Route(HttpMethodType.Get, "/users/login", Login));
            routeTable.Add(new Route(HttpMethodType.Post, "/user/login", DoLogin));
            routeTable.Add(new Route(HttpMethodType.Get, "/contact", Contact));
            routeTable.Add(new Route(HttpMethodType.Get, "/favicon.ico", FavIcon));

            var httpServer = new HttpServer(1234, routeTable);
            await httpServer.StartAsync();
        }

        private static HttpResponse FavIcon(HttpRequest request)
        {
            var byteContent = File.ReadAllBytes("wwwroot/favicon.ico");
            return new FileResponse(byteContent, "image/x-icon");
        }

        private static HttpResponse Contact(HttpRequest request)
        {
            return new HtmlResponse("<h1>contact</h1>");
        }

        public static HttpResponse Index(HttpRequest request)
        {
            return new HtmlResponse("<h1>home page</h1>");
        }

        public  static HttpResponse Login(HttpRequest request)
        {
            return new HtmlResponse("<h1>login page</h1>");
        }

        public static HttpResponse DoLogin(HttpRequest request)
        {
           return new HtmlResponse("<h1>login page</h1>");
        }
    }
}
