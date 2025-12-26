using Microsoft.EntityFrameworkCore;
using Scraping_Egy_Bus.Data;

namespace Scraping_Egy_Bus.Scraping
{
    public class DeleteTempTable
    {
        private readonly DataContext _context;

        public DeleteTempTable(DataContext context)
        {
            _context = context;
        }
        public void DeleteTemp()
        {
            _context.TempTrips.ExecuteDelete();
            _context.SaveChanges();
        }
    }
}
