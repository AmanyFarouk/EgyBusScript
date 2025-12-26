namespace Scraping_Egy_Bus.Entities
{
    public class Trip : BaseModel
    {
        public int TripId { get; set; }
        public int CompanyId { get; set; }

        //   public string? CompanyTripId { get; set; }

        public int DepartureStationId { get; set; }
        public int ArrivalStationId { get; set; }

        // public DateTime TripDate { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public DateTime? ArrivalDateTime { get; set; }
        public int? Duration { get; set; }

        public int? BusTypeId { get; set; }
        public decimal Price { get; set; }

        public List<string>? Features { get; set; }

        // public int? AvailableSeats { get; set; } 
        public int? TotalSeats { get; set; }

        public bool IsActive { get; set; } = false;
        public DateTime ScrapedAt { get; set; }
        public string? ExternalUrl { get; set; }

        public Company Company { get; set; } = null!;

        public Station DepartureStation { get; set; } = null!;
        public Station ArrivalStation { get; set; } = null!;
        public BusType BusType { get; set; } = null!;

        public ICollection<TripStop> TripStops { get; set; } = new List<TripStop>();
        public ICollection<AdditionalTripData> AdditionalData { get; set; } = new List<AdditionalTripData>();
    }
}
