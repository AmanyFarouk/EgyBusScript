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
            var runAndSaveScript = new SaveScrapingDataToTempTables(_context);
            RecurringJob.AddOrUpdate(() => runAndSaveScript.Save(), Cron.Minutely);   
        }

    }
}
