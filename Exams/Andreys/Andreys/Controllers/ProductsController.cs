using Andreys.Models;
using Andreys.Services;
using Andreys.ViewModels.Products;
using SIS.HTTP;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Andreys.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public HttpResponse Add()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(ProductInputInfo input)
        {
            productsService.Add(input);

            return this.Redirect("/");
        }

        public HttpResponse Details(string id)
        {
            var model = productsService.GetProduct(id);
            return this.View(model);
        }

        public HttpResponse Delete(string id)
        {
            productsService.DeleteProduct(id);
            return this.Redirect("/");
        }
    }
}
