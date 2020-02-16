using SharedTrip.ViewModels.Trips;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.Services
{
    public interface ITripsService
    {
        void AddTrip(TripInputModel input);

        AllTripsInfoModel GetAllTrips();

        TripInfoModel GetTrip(string id);

        void AddUser(string tripId, string userId);

        void ReduceSeatCount(string tripId);

        bool IsExistingUserTrip(string userId, string tripId);
    }
}
