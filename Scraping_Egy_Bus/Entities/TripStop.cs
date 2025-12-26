using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraping_Egy_Bus.Entities
{
    public class TripStop :BaseModel
    {
        public int TripStopId { get; set; }
        public int TripId { get; set; }
        public int StationId { get; set; }
        public int? StopOrder { get; set; }
        public TimeSpan? ArrivalTime { get; set; }
        public TimeSpan? DepartureTime { get; set; }

        // Foreign Key Navigation
        public Trip Trip { get; set; } = null!;

        //  public ICollection<Station> Stations { get; set; }
        public Station Station { get; set; } = null!;
    }
}
