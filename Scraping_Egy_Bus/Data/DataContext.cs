using Microsoft.EntityFrameworkCore;
using Scraping_Egy_Bus.Entities;
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
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
        //egy bus entity
        public DbSet<TempTrip> TempTrips { get; set; }

        //db entities

        public DbSet<Company> Companies { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<BusType> BusTypes { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<TripStop> TripStops { get; set; }
        public DbSet<AdditionalTripData> AdditionalTripData { get; set; }
    }
}
