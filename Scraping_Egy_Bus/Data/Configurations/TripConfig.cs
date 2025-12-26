using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scraping_Egy_Bus.Entities;

namespace Scraping_Egy_Bus.Configurations
{
    public class TripConfig : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> builder)
        {
            builder.HasKey(e => e.TripId);


            builder.Property(e => e.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(e => e.DepartureDateTime)
                 .IsRequired();

            builder.Property(e => e.ArrivalDateTime)
               .IsRequired(false);

            builder.Property(e => e.Duration)
               .IsRequired(false);

            builder.Property(e => e.IsActive)
                .HasDefaultValue(false);

            builder.Property(e => e.ScrapedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(e => e.ExternalUrl)
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.Property(e => e.Features)
                   .HasColumnType("nvarchar(max)")
                   .IsRequired(false);

            // Relationship: Company → Trips (Required)
            builder.HasOne(t => t.Company)
                .WithMany(c => c.Trips)
                .HasForeignKey(t => t.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);


            // Relationship: BusType → Trips (optional)
            builder.HasOne(t => t.BusType)
                .WithMany(bt => bt.Trips)
                .HasForeignKey(t => t.BusTypeId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            // Relationship: Departure Station → Trips (required)
            builder.HasOne(t => t.DepartureStation)
                .WithMany(s => s.DepartureTrips)
                .HasForeignKey(t => t.DepartureStationId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relationship: Arrival Station → Trips (required)
            builder.HasOne(t => t.ArrivalStation)
                .WithMany(s => s.ArrivalTrips)
                .HasForeignKey(t => t.ArrivalStationId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes for fast searching (VERY IMPORTANT!)

            builder.HasIndex(e => new { e.DepartureStationId, e.ArrivalStationId, e.DepartureDateTime })
                .HasDatabaseName("IX_Trip_Search");


            builder.HasIndex(e => new { e.CompanyId, e.DepartureDateTime })
                .HasDatabaseName("IX_Trip_Company_Date");

            builder.HasIndex(e => e.IsActive)
                .HasDatabaseName("IX_Trip_Active");

            builder.HasIndex(e => new { e.CompanyId, e.DepartureStationId, e.ArrivalStationId, e.DepartureDateTime })
                .IsUnique()
                .HasDatabaseName("UX_Trip_Unique");


        }
    }
}