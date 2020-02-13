using SIS.HTTP;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP
{
    public class Route
    {
        public Route(HttpMethodType httpMethod, string Path, Func<HttpRequest, HttpResponse> action)
        {
            this.Path = Path;
            this.HttpMethod = httpMethod;
            this.Action = action;
        }
        public string Path { get; set; }

        public HttpMethodType HttpMethod { get; set; }

        public Func<HttpRequest, HttpResponse> Action { get; set; }
    }
}
