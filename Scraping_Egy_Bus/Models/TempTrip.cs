namespace Scraping_Egy_Bus.Models
{
    public class TempTrip
    {
        public int Id { get; set; }
        public int FromCity { get; set; }
        public int ToCity { get; set; }
        public string TripCode { get; set; }
        public string FromCityName { get; set; }
        public string ToCityName { get; set; }
        public decimal Price { get; set; }
        public string DepartureTime { get; set; }
        public string TripDate { get; set; }
        public string CompanyName { get; set; }
        public string BookingUrl { get; set; }
        public string Features { get; set; }
    }
}
