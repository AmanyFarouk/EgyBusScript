using Microsoft.EntityFrameworkCore;
using Scraping_Egy_Bus.Data;

namespace Scraping_Egy_Bus.Scraping
{
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
            var allTrips = await scraper.ScrapeRouteAsync(17, 1, DateTime.Today);
            //var allTrips =await scraper.ScrapeDaysAsync(DateTime.Today, 1);
            Console.WriteLine(allTrips.Count);
            _context.TempTrips.AddRangeAsync(allTrips);
            _context.SaveChanges();
        }
    }
}
