using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.ViewModels.Trips
{
    public class AllTripsInfoModel
    {
        public ICollection<TripInfoModel> Trips { get; set; }
    }
}
