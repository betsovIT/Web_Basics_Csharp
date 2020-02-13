using IRunes.Models;
using IRunes.ViewModels.Tracks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRunes.Services
{
    public class TrackService : ITrackService
    {
        private readonly ApplicationDbContext db;

        public TrackService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void Create(string albumId, string name, string link, decimal price)
        {
            var track = new Track()
            {
                AlbumId = albumId,
                Name = name,
                Link = link,
                Price = price
            };

            db.Tracks.Add(track);

            var alltrackPricesSum = this.db.Tracks.Where(x => x.AlbumId == albumId).Sum(x => x.Price) + price;
            var album = this.db.Albums.FirstOrDefault(x => x.Id == albumId);

            album.Price = alltrackPricesSum * 0.87m;

            db.SaveChanges();


        }

        public DetailsViewModel GetDetails(string trackId)
        {
            var track = db.Tracks.Where(x => x.Id == trackId).Select(x => new DetailsViewModel
            {
                Name = x.Name,
                Link = x.Link,
                AlbumId = x.AlbumId,
                Price = x.Price
            }).FirstOrDefault();

            return track;
        }
    }
}
