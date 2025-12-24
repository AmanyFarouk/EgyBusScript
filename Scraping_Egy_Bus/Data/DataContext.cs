using Microsoft.EntityFrameworkCore;
using Scraping_Egy_Bus.Models;

namespace Scraping_Egy_Bus.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<TempTrip> TempTrips { get; set; }
    }
}
