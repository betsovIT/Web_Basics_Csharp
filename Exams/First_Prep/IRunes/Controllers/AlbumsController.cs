using IRunes.Models;
using IRunes.Services;
using IRunes.ViewModels.Albums;
using SIS.HTTP;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace IRunes.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly IAlbumService albumService;

        public AlbumsController(IAlbumService albumService)
        {
            this.albumService = albumService;
        }

        public HttpResponse All()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModel = new AllAlbumsViewModel()
            {
                Albums = albumService.GetAll()
            };

            return this.View(viewModel);
        }

        public HttpResponse Create()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(CreateInputModel input)
        {            
            if (input.Name.Length < 4 || input.Name.Length > 20)
            {
                return this.Error("Invalid album name");
            }

            if (string.IsNullOrEmpty(input.Cover))
            {
                return this.Error("Cover is required");
            }

            albumService.Create(input.Name, input.Cover);

            return this.Redirect("/Albums/All");
        }

        public HttpResponse Details(string id)
        {
            var viewModel = albumService.GetDetails(id);
            return this.View(viewModel);
        }
    }
}
