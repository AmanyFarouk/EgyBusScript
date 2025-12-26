using Hangfire;
using Microsoft.EntityFrameworkCore;
using Scraping_Egy_Bus.Data;
using Scraping_Egy_Bus.Entities;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Scraping_Egy_Bus.Scraping
{
    public class MappingTempToDbTables
    {
        private readonly DataContext _context;

        public MappingTempToDbTables(DataContext context)
        {
            _context = context;
        }
        private async Task<City> GetOrCreateCityAsync(string cityName)
        {
            var city = await _context.Cities
                .FirstOrDefaultAsync(c => c.CityName == cityName);

            if (city != null)
                return city;

            city = new City
            {
                CityName = cityName
            };

            _context.Cities.Add(city);
            await _context.SaveChangesAsync();

            return city;
        }

        private async Task<Station> GetOrCreateStationAsync(int cityId)
        {
            var station = await _context.Stations
                .FirstOrDefaultAsync(s => s.CityId == cityId);

            if (station != null)
                return station;

            station = new Station
            {
                CityId = cityId,
                StationName =_context.Cities.Find(cityId).CityName,
                CompanyId=_context.Companies.FirstOrDefault(c=>c.CompanyName=="EG-Bus").CompanyId
            };

            _context.Stations.Add(station);
            await _context.SaveChangesAsync();

            return station;
        }


        public async Task MapTables()
        {
            var tempTrips = await _context.TempTrips.ToListAsync();
            foreach (var trip in tempTrips)
            {

                var fromcity =await GetOrCreateCityAsync(trip.FromCityName);
                var tocity = await GetOrCreateCityAsync(trip.ToCityName);

                var fromStation = await GetOrCreateStationAsync(fromcity.CityId);
                var toStation = await GetOrCreateStationAsync(tocity.CityId);


               var tripDateTime = (trip.TripDate.Date) + (trip.DepartureTime);
               bool exists = await _context.Trips.AnyAsync(t =>
               t.DepartureStationId == fromStation.StationId &&
               t.ArrivalStationId == toStation.StationId &&
               t.DepartureDateTime == tripDateTime);
               if (exists)
               {
                    continue;
               }

               var newtrip = new Trip
               {
                   CompanyId=_context.Companies.FirstOrDefault(c=>c.CompanyName=="EG-BUS").CompanyId,
                   DepartureStationId=fromStation.StationId,
                   ArrivalStationId=toStation.StationId,
                   DepartureDateTime= (DateTime)tripDateTime,
                   Price=trip.Price,
                   Features=trip.Features.Split(',').ToList(),
                   ScrapedAt=DateTime.Now,
                   ExternalUrl=trip.BookingUrl,
                   IsActive=true
               };
               _context.Trips.Add(newtrip);

            }
            await _context.SaveChangesAsync();

            var deleteTemp = new DeleteTempTable(_context);
            BackgroundJob.Enqueue(()=>deleteTemp.DeleteTemp());
        }

    }
}
