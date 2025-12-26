using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scraping_Egy_Bus.Entities;

namespace Scraping_Egy_Bus.Configurations
{
    public class TripStopConfig : IEntityTypeConfiguration<TripStop>
    {
        public void Configure(EntityTypeBuilder<TripStop> builder)
        {
            builder.HasKey(e => e.TripStopId);

            builder.Property(e => e.StopOrder)
                .IsRequired(false);

            builder.Property(e => e.ArrivalTime)
                .IsRequired(false);

            builder.Property(e => e.DepartureTime)
                .IsRequired(false);

            // Relationship: Trip → TripStops
            builder.HasOne(ts => ts.Trip)
                .WithMany(t => t.TripStops)
                .HasForeignKey(ts => ts.TripId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship: Station → TripStops
            builder.HasOne(ts => ts.Station)
                .WithMany(s => s.TripStops)
                .HasForeignKey(ts => ts.StationId)
                .OnDelete(DeleteBehavior.Restrict);

            // Index for ordering
            builder.HasIndex(e => e.TripId)
               .HasDatabaseName("IX_TripStop_Trip");

            // builder.HasIndex(e => new { e.TripId, e.StopOrder });

            builder.HasIndex(e => new { e.TripId, e.StationId })
                .IsUnique()
                .HasDatabaseName("UX_TripStop_Trip_Station");


        }
    }
}
