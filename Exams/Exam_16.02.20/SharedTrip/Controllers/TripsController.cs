using SharedTrip.Services;
using SharedTrip.ViewModels.Trips;
using SIS.HTTP;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SharedTrip.Controllers
{
    public class TripsController : Controller
    {
        private readonly ITripsService tripsService;

        public TripsController(ITripsService tripsService)
        {
            this.tripsService = tripsService;
        }

        public HttpResponse All()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/");
            }

            var model = tripsService.GetAllTrips();

            return this.View(model);
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
        public HttpResponse Add(TripInputModel input)
        {
            DateTime parsedDate;
            int seats;
            if (!int.TryParse(input.Seats, out seats))
            {
                return this.Redirect("/Trips/Add");
            }
            if (string.IsNullOrWhiteSpace(input.StartPoint))
            {
                return this.Redirect("/Trips/Add");
            }
            if (string.IsNullOrWhiteSpace(input.EndPoint))
            {
                return this.Redirect("/Trips/Add");
            }
            if (seats< 2 || seats > 6)
            {
                return this.Redirect("/Trips/Add");
            }
            if (string.IsNullOrWhiteSpace(input.Description) || input.Description.Length > 80)
            {
                return this.Redirect("/Trips/Add");
            }
            if (!DateTime.TryParseExact(input.DepartureTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture,DateTimeStyles.None, out parsedDate))
            {
                return this.Redirect("/Trips/Add");
            }
            if (parsedDate < DateTime.Now)
            {
                return this.Redirect("/Trips/Add");
            }
            tripsService.AddTrip(input);
            return this.Redirect("/Trips/All");
        }

        public HttpResponse Details(string id)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/");
            }

            var model = tripsService.GetTrip(id);

            return this.View(model);
        }
        
        public HttpResponse AddUserToTrip(string tripId)
        {
            if (!this.IsUserLoggedIn())
            {
                this.Redirect("/");
            }

            string user = this.User;

            if (tripsService.IsExistingUserTrip(user, tripId))
            {
                return this.Redirect("/Trips/Details?id=tripId");
            }

            tripsService.AddUser(tripId, user);
            tripsService.ReduceSeatCount(tripId);

            return this.Redirect("/");
        }


    }
}
