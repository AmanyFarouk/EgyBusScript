using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraping_Egy_Bus.Entities
{
    public class Station : BaseModel
    {
        public int StationId { get; set; }
        public int CityId { get; set; }
        public int CompanyId { get; set; }
        public string StationName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        // public Location? Location { get; set; }  

        public City City { get; set; } = null!;
        public Company Company { get; set; } = null!;

        public ICollection<Trip> DepartureTrips { get; set; } = new List<Trip>();
        public ICollection<Trip> ArrivalTrips { get; set; } = new List<Trip>();
        public ICollection<TripStop> TripStops { get; set; } = new List<TripStop>();
    }
}
