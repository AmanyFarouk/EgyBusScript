using Hangfire;
using Hangfire.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scraping_Egy_Bus.Data;
using Scraping_Egy_Bus.Models;
using Scraping_Egy_Bus.Scraping;

namespace Scraping_Egy_Bus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScrapingController : ControllerBase
    {
        private readonly DataContext _context;

        public ScrapingController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task Index() 
        {
            //scraping trips monthly job
            var runAndSaveScript = new SaveScrapingDataToTempTables(_context);
            RecurringJob.AddOrUpdate(() => runAndSaveScript.Save(), Cron.Minutely);


            //delete old trips job
            RecurringJob.AddOrUpdate(() => DeleteOldTripsAsync(), Cron.Daily);

        }
        [ApiExplorerSettings(IgnoreApi =true)]
        public async Task DeleteOldTripsAsync()
        {
            Console.WriteLine("delete old trips");
            var oldTrips = _context.TempTrips.Where(t => t.TripDate.Date < DateTime.Now.Date).ToList();
            if (!oldTrips.Any()) 
            {
                return;
            }
            _context.TempTrips.RemoveRange(oldTrips);
            await _context.SaveChangesAsync();
        }
    }
}
