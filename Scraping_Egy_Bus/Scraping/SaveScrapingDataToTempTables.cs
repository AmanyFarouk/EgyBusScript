using Hangfire;
using Microsoft.EntityFrameworkCore;
using Scraping_Egy_Bus.Data;
using Scraping_Egy_Bus.Models;

namespace Scraping_Egy_Bus.Scraping
{
    public record TripUniqueKey
    (
        string TripCode,
        DateTime TripDate,
        TimeSpan ? DepartureTime
    );
    public class SaveScrapingDataToTempTables
    {
        private readonly DataContext _context;

        public SaveScrapingDataToTempTables(DataContext context)
        {
            _context = context;
        }
        public async Task Save()
        {
            var scraper = new EgBusScraper();
            var allTrips = await scraper.ScrapeRouteAsync(1, 17, new DateTime(2025,12,28));//return trips in one day between cairo and assiut
            //var allTrips =await scraper.ScrapeAllTripsAsync( 30); // return all trips in 30 days
            
            var existingKeys = await _context.TempTrips.Select(t => new TripUniqueKey(t.TripCode, t.TripDate, t.DepartureTime)).ToListAsync();
            var existinSet=new HashSet<TripUniqueKey>(existingKeys);      

            var newTrips = allTrips.Where(trip => !existinSet.Contains(new TripUniqueKey(trip.TripCode, trip.TripDate, trip.DepartureTime))).ToList();
            if (newTrips.Any())
            {
            await _context.TempTrips.AddRangeAsync(newTrips);
            await _context.SaveChangesAsync();
            }
            var Map = new MappingTempToDbTables(_context);
            BackgroundJob.Enqueue(() => Map.MapTables());
        }
    }
}
