using SharedTrip.Models;
using SharedTrip.ViewModels.Trips;
using System;
using System.Globalization;
using System.Linq;

namespace SharedTrip.Services
{
    public class TripsService : ITripsService
    {
        private readonly ApplicationDbContext db;

        public TripsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void AddTrip(TripInputModel input)
        {
            var trip = new Trip
            {
                StartPoint = input.StartPoint,
                EndPoint = input.EndPoint,
                DepartureTime = DateTime.ParseExact(input.DepartureTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
                Seats = int.Parse(input.Seats),
                Description = input.Description,
                ImagePath = input.ImagePath
            };

            db.Trips.Add(trip);
            db.SaveChanges();
        }

        public void AddUser(string tripId, string userId)
        {
            var userTrip = new UserTrip
            {
                UserId = userId,
                TripId = tripId
            };

            this.db.UsersTrips.Add(userTrip);
            this.db.SaveChanges();
        }

        public AllTripsInfoModel GetAllTrips()
        {
            var trips = db.Trips.Select(t => new TripInfoModel
            {
                Id = t.Id,
                StartPoint = t.StartPoint,
                EndPoint = t.EndPoint,
                DepartureTime = t.DepartureTime.ToString("dd.MM.yyyy HH:mm"),
                Seats = t.Seats,
                ImagePath = t.ImagePath,
                Description = t.Description
            }).ToList();

            var model = new AllTripsInfoModel();

            model.Trips = trips;

            return model;
        }

        public TripInfoModel GetTrip(string id)
        {
            var trip = this.db.Trips.Where(t => t.Id == id).Select(t => new TripInfoModel
            {
                StartPoint = t.StartPoint,
                EndPoint = t.EndPoint,
                DepartureTime = t.DepartureTime.ToString("dd.MM.yyyy HH: mm"),
                Description = t.Description,
                Seats = t.Seats,
                ImagePath = t.ImagePath,
                Id = t.Id
            }).FirstOrDefault();

            return trip;
        }

        public bool IsExistingUserTrip(string userId, string tripId)
        {
            return this.db.UsersTrips.Any(ut => ut.UserId == userId && ut.TripId == tripId);
        }

        public void ReduceSeatCount(string tripId)
        {
            var trip = db.Trips.FirstOrDefault(t => t.Id == tripId);

            trip.Seats -= 1;

            this.db.SaveChanges();
        }
    }
}
