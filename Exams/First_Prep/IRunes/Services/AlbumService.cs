using IRunes.Models;
using IRunes.ViewModels.Albums;
using IRunes.ViewModels.Tracks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRunes.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly ApplicationDbContext db;

        public AlbumService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void Create(string name, string cover)
        {
            var album = new Album()
            {
                Name = name,
                Cover = cover,
                Price = 0M
            };

            db.Albums.Add(album);
            db.SaveChanges();
        }

        public IEnumerable<AlbumInfoViewModel> GetAll()
        {
            return this.db.Albums
                .Select(a => new AlbumInfoViewModel()
                {
                    Id = a.Id,
                    Name = a.Name
                }).ToList();
        }

        public AlbumDetailsViewModel GetDetails(string id)
        {
            return this.db.Albums
                .Where(a => a.Id == id)
                .Select(a => new AlbumDetailsViewModel()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Price = a.Price,
                    Cover = a.Cover,
                    Tracks = a.Tracks.Select(t => new TrackInfoViewModel()
                    {
                        Id = t.Id,
                        Name = t.Name
                    })
                }).FirstOrDefault();
        }
    }
}
